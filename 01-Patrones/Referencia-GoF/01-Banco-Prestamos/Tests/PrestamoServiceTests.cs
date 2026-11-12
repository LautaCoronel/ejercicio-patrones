using DomainModel;
using BLL;
using Xunit;

namespace Tests;

public class PrestamoServiceTests
{
    [Theory]
    [InlineData(50000, "Ejecutivo")]
    [InlineData(300000, "Lider")]
    [InlineData(800000, "Gerente")]
    [InlineData(2000000, "Director")]
    public void ApruebaElNivelCorrecto(double monto, string esperado)
    {
        var service = AppFactory.CrearPrestamoService();
        var p = service.Solicitar(new Cliente { Nombre = "Test" }, (decimal)monto);
        Assert.Equal(esperado, p.AprobadoPor);
        Assert.True(p.Aprobado);
    }
}
