using System.Collections.Generic;
using DomainModel;
using BLL;
using BLL.Patrones;
using Xunit;

namespace Tests;

public class YaLlegaTests
{
    [Theory]
    [InlineData(10, 1000)]
    [InlineData(50, 1250)]
    [InlineData(90, 1600)]
    public void EnvioPorDemanda_SegunDemanda(int demanda, int esperado)
        => Assert.Equal(esperado, new EnvioPorDemanda().Calcular(1000m, demanda));

    [Fact]
    public void ModoPromocion_SiempreGratis()
        => Assert.Equal(0m, new ModoPromocion().Calcular(1000m, 90));

    [Fact]
    public void Proxy_BloqueaPremiumSiPlanClasico()
    {
        var proxy = new MenuProxy(Plan.Clasico);
        var r = proxy.Consultar(new Producto { Nombre = "X", EsPremium = true });
        Assert.StartsWith("BLOQUEADO", r);
    }

    [Fact]
    public void Proxy_PermitePremiumSiPlanPremium()
    {
        var proxy = new MenuProxy(Plan.Premium);
        var r = proxy.Consultar(new Producto { Nombre = "X", EsPremium = true });
        Assert.StartsWith("descargado", r);
    }

    [Fact]
    public void Visitor_ExportaProductoYCliente()
    {
        var export = AppFactory.CrearExportService();
        var txt = export.Exportar(new List<IExportable>
        {
            new Producto { Nombre = "Pizza", EsPremium = false },
            new Cliente { Nombre = "Ana", Plan = Plan.Premium }
        });
        Assert.Contains("PRODUCTO;Pizza", txt);
        Assert.Contains("CLIENTE;Ana", txt);
    }
}
