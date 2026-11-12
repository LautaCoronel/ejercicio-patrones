using BLL.Patrones;
using DAL;
using DomainModel;
using Services;
using System.Collections.Generic;

namespace BLL;

public class PedidoService
{
    private readonly IRepositorio<Pedido> _repo;
    private readonly ILogger _logger;
    private readonly IMenu _menu;
    private IEstrategiaEnvio _estrategia = new EnvioPorDemanda();

    public PedidoService(IRepositorio<Pedido> repo, ILogger logger, Plan plan)
    {
        _repo = repo; _logger = logger;
        _menu = new MenuProxy(plan); // Proxy con el plan del cliente
    }

    public void SetModoPromocion(bool activo)
        => _estrategia = activo ? (IEstrategiaEnvio)new ModoPromocion() : new EnvioPorDemanda();

    public string Agregar(Cliente c, Producto p, decimal tarifa, int demanda)
    {
        var envio = _estrategia.Calcular(tarifa, demanda);  // Strategy
        var resultado = _menu.Consultar(p);                  // Proxy
        _repo.Agregar(new Pedido { Producto = p.Nombre, Envio = envio, ClienteNombre = c.Nombre });
        _logger.Log(c.Nombre + " -> " + resultado + " envio $" + envio);
        return resultado + " envio $" + envio;
    }

    public List<Pedido> Historial() => _repo.ObtenerTodos();
}
