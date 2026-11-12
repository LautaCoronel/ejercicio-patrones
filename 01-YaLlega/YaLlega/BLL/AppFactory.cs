using DAL;
using DomainModel;
using Services;

namespace BLL;

public static class AppFactory
{
    public static PedidoService CrearPedidoService(Plan plan)
        => new PedidoService(new RepositorioMemoria<Pedido>(), new ConsoleLogger(), plan);

    public static ExportService CrearExportService() => new ExportService();
}
