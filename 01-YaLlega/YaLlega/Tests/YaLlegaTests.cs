using System.Collections.Generic;
using System.IO;
using BLL;
using BLL.Patrones;
using DAL;
using DomainModel;
using Xunit;

namespace Tests;

// Un caso de prueba por cada escenario del enunciado.
public class YaLlegaTests
{
    private static readonly Cliente Clasico = new Cliente { Id = 1, Nombre = "Ana", Plan = Plan.Clasico };
    private static readonly Cliente Premium = new Cliente { Id = 2, Nombre = "Beto", Plan = Plan.Premium };

    private static PedidoService Servicio(Cliente c, out CatalogoRemoto cat, out RepositorioMemoria<Pedido> repo)
    {
        cat = AppFactory.CrearCatalogo();
        repo = new RepositorioMemoria<Pedido>();
        return AppFactory.CrearPedidoService(c, cat, repo);
    }

    // ---------- Strategy: costo de envio ----------
    [Fact] public void DemandaBaja_TarifaNormal() => Assert.Equal(1000m, new TarifaNormal().Calcular(1000m));
    [Fact] public void DemandaMedia_Recargo25() => Assert.Equal(1250m, new RecargoDemandaMedia().Calcular(1000m));
    [Fact] public void DemandaAlta_Recargo60() => Assert.Equal(1600m, new RecargoDemandaAlta().Calcular(1000m));
    [Fact] public void Promocion_SiempreGratis() => Assert.Equal(0m, new EnvioPromocion().Calcular(1000m));

    [Fact]
    public void CambiaElEnvio_ConElPedidoAbierto_YQuedanTodasLasTarifas()
    {
        var s = Servicio(Clasico, out _, out _);
        var p = s.AbrirPedido();
        s.AgregarProducto(1);
        s.MedirDemanda(NivelDemanda.Media);
        s.AgregarProducto(2);
        s.MedirDemanda(NivelDemanda.Alta);

        Assert.Equal(3, p.Tarifas.Count);   // normal, media, alta
        Assert.Equal(1600m, p.Envio);
    }

    [Fact]
    public void OperadorFuerzaPromocion_IgnoraLaDemanda()
    {
        var s = Servicio(Clasico, out _, out _);
        var p = s.AbrirPedido();
        s.ForzarModo(new EnvioPromocion());
        s.MedirDemanda(NivelDemanda.Alta);
        Assert.Equal(0m, p.Envio);

        s.LiberarModo();
        s.MedirDemanda(NivelDemanda.Alta);
        Assert.Equal(1600m, p.Envio);
    }

    // ---------- Proxy: plan y cache ----------
    [Fact]
    public void ProductoFueraDelPlan_SeRechazaYQuedaRegistrado()
    {
        var s = Servicio(Clasico, out _, out _);
        var p = s.AbrirPedido();
        Assert.False(s.AgregarProducto(4));      // Sushi requiere Premium
        Assert.Empty(p.Items);
        Assert.Single(p.Rechazados);
    }

    [Fact]
    public void PlanPremium_PuedePedirTodo()
    {
        var s = Servicio(Premium, out _, out _);
        s.AbrirPedido();
        Assert.True(s.AgregarProducto(4));
    }

    [Fact]
    public void SegundaConsulta_SaleDelDispositivo_SinDescargar()
    {
        var s = Servicio(Clasico, out var cat, out _);
        var p = s.AbrirPedido();
        s.AgregarProducto(1);
        s.AgregarProducto(1);

        Assert.Equal("SERVIDOR", p.Items[0].Origen);
        Assert.Equal("DISPOSITIVO", p.Items[1].Origen);
        Assert.Equal(1, cat.Descargas);
    }

    [Fact]
    public void Cache_GuardaSoloLosUltimos5()
    {
        var cat = AppFactory.CrearCatalogo();
        var proxy = new CatalogoProxy(cat, Plan.Premium);
        for (int id = 1; id <= 6; id++) proxy.ObtenerProducto(id);   // 6 distintos: se va el 1

        proxy.ObtenerProducto(6);
        Assert.Equal("DISPOSITIVO", proxy.UltimoOrigen);
        proxy.ObtenerProducto(1);
        Assert.Equal("SERVIDOR", proxy.UltimoOrigen);
    }

    // ---------- Visitor: exportacion ----------
    [Fact]
    public void Exporta_ProductosClientesYPedidos_ConEncabezado()
    {
        var export = AppFactory.CrearExportService();
        var s = Servicio(Clasico, out var cat, out var repo);
        s.AbrirPedido(); s.AgregarProducto(1); s.CerrarPedido();

        string prod = export.ArmarTexto(new List<IExportable> { cat.ObtenerProducto(1) });
        string cli = export.ArmarTexto(new List<IExportable> { Clasico });
        string ped = export.ArmarTexto(new List<IExportable>(repo.ObtenerTodos()));

        Assert.StartsWith("ID;NOMBRE;PRECIO;PLAN_MINIMO", prod);
        Assert.StartsWith("ID;NOMBRE;PLAN", cli);
        Assert.StartsWith("ID;CLIENTE;PRODUCTOS;ENVIO;TOTAL", ped);
        Assert.Contains("Pizza", ped);
    }

    // ---------- Persistencia ----------
    [Fact]
    public void Persistencia_GuardaClienteProductosTarifasOrigenYRechazados()
    {
        string ruta = Path.GetTempFileName();
        var cat = AppFactory.CrearCatalogo();
        var s = AppFactory.CrearPedidoService(Clasico, cat, new RepositorioPedidosArchivo(ruta));
        s.AbrirPedido();
        s.AgregarProducto(1);
        s.AgregarProducto(4);
        s.MedirDemanda(NivelDemanda.Alta);
        s.CerrarPedido();

        string txt = File.ReadAllText(ruta);
        Assert.Contains("CLIENTE: Ana", txt);
        Assert.Contains("PRODUCTO: Pizza", txt);
        Assert.Contains("MENU DESDE: SERVIDOR", txt);
        Assert.Contains("TARIFA: Demanda alta (+60%)", txt);
        Assert.Contains("RECHAZADO POR PLAN: Sushi", txt);
        File.Delete(ruta);
    }

    [Fact]
    public void SinPedidoAbierto_LanzaExcepcion()
    {
        var s = Servicio(Clasico, out _, out _);
        Assert.Throws<System.InvalidOperationException>(() => s.AgregarProducto(1));
    }
}
