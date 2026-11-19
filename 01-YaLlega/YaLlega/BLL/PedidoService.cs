using System;
using BLL.Patrones;
using DAL;
using DomainModel;
using Services;

namespace BLL;

// Arma el pedido de un cliente: usa Strategy (envio), Proxy (catalogo) y Repository (persistencia).
public class PedidoService
{
    public const decimal TarifaBase = 1000m;
    private static int _ultimoId = 0;

    private readonly Cliente _cliente;
    private readonly ICatalogo _catalogo;
    private readonly IRepositorio<Pedido> _repo;
    private readonly ILogger _logger;

    private IEstrategiaEnvio _estrategia = new TarifaNormal();
    private bool _modoForzado = false;
    private Pedido _pedido;

    public PedidoService(Cliente cliente, ICatalogo catalogo, IRepositorio<Pedido> repo, ILogger logger)
    {
        _cliente = cliente;
        _catalogo = catalogo;
        _repo = repo;
        _logger = logger;
    }

    public IEstrategiaEnvio EstrategiaActual => _estrategia;

    public Pedido AbrirPedido()
    {
        _pedido = new Pedido { Id = ++_ultimoId, Cliente = _cliente };
        RegistrarTarifa();
        _logger.Log($"Pedido #{_pedido.Id} abierto para {_cliente.Nombre}");
        return _pedido;
    }

    // Devuelve false si el producto no esta en el plan (queda registrado como rechazado).
    public bool AgregarProducto(int idProducto)
    {
        ValidarAbierto();
        try
        {
            Producto p = _catalogo.ObtenerProducto(idProducto);     // Proxy
            string origen = (_catalogo as CatalogoProxy)?.UltimoOrigen ?? "SERVIDOR";
            _pedido.Items.Add(new ItemPedido { Producto = p, Origen = origen });
            _logger.Log($"+ {p.Nombre} (menu desde {origen})");
            return true;
        }
        catch (ProductoNoPermitidoException ex)
        {
            _pedido.Rechazados.Add(ex.Message);
            _logger.Log("RECHAZADO: " + ex.Message);
            return false;
        }
    }

    // El sistema mide la demanda; si el operador no forzo un modo, cambia la estrategia.
    public void MedirDemanda(NivelDemanda demanda)
    {
        if (_modoForzado) return;
        switch (demanda)
        {
            case NivelDemanda.Baja: CambiarEstrategia(new TarifaNormal()); break;
            case NivelDemanda.Media: CambiarEstrategia(new RecargoDemandaMedia()); break;
            case NivelDemanda.Alta: CambiarEstrategia(new RecargoDemandaAlta()); break;
        }
    }

    // Boton del operador: fuerza un modo (por ejemplo Promocion) sin importar la demanda.
    public void ForzarModo(IEstrategiaEnvio estrategia)
    {
        _modoForzado = true;
        CambiarEstrategia(estrategia);
    }

    // Vuelve al calculo automatico por demanda.
    public void LiberarModo() => _modoForzado = false;

    public Pedido CerrarPedido()
    {
        ValidarAbierto();
        _pedido.Abierto = false;
        _repo.Agregar(_pedido);     // persistencia
        _logger.Log($"Pedido #{_pedido.Id} cerrado. Envio ${_pedido.Envio} | Total ${_pedido.Total}");
        return _pedido;
    }

    private void CambiarEstrategia(IEstrategiaEnvio nueva)
    {
        if (nueva == null) throw new ArgumentNullException(nameof(nueva));
        _estrategia = nueva;
        if (_pedido != null && _pedido.Abierto) RegistrarTarifa();   // cambia con el pedido abierto
    }

    private void RegistrarTarifa()
    {
        decimal monto = _estrategia.Calcular(TarifaBase);
        _pedido.Tarifas.Add(new TarifaAplicada { Modo = _estrategia.Nombre, Monto = monto });
        _logger.Log($"Envio: {_estrategia.Nombre} -> ${monto}");
    }

    private void ValidarAbierto()
    {
        if (_pedido == null || !_pedido.Abierto)
            throw new InvalidOperationException("No hay un pedido abierto.");
    }
}
