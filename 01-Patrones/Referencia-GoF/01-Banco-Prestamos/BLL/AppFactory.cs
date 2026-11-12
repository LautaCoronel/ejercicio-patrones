using DomainModel;
using DAL;
using Services;

namespace BLL;

public static class AppFactory
{
    public static PrestamoService CrearPrestamoService()
        => new PrestamoService(new RepositorioMemoria<Prestamo>(), new ConsoleLogger());
}
