using BLL.Patrones;
using DAL;
using DomainModel;
using Services;

namespace BLL;

// Arma los objetos con sus dependencias (la UI no hace new de nada interno).
public static class AppFactory
{
    public static CatalogoRemoto CrearCatalogo()
    {
        var c = new CatalogoRemoto();
        c.Cargar(new Producto { Id = 1, Nombre = "Pizza", Precio = 8000, PlanMinimo = Plan.Clasico });
        c.Cargar(new Producto { Id = 2, Nombre = "Empanadas", Precio = 6000, PlanMinimo = Plan.Clasico });
        c.Cargar(new Producto { Id = 3, Nombre = "Hamburguesa", Precio = 9000, PlanMinimo = Plan.Plus });
        c.Cargar(new Producto { Id = 4, Nombre = "Sushi", Precio = 15000, PlanMinimo = Plan.Premium });
        c.Cargar(new Producto { Id = 5, Nombre = "Helado", Precio = 5000, PlanMinimo = Plan.Clasico });
        c.Cargar(new Producto { Id = 6, Nombre = "Milanesa", Precio = 7000, PlanMinimo = Plan.Clasico });
        return c;
    }

    public static PedidoService CrearPedidoService(Cliente cliente, ICatalogo catalogoRemoto,
                                                   IRepositorio<Pedido> repo)
        => new PedidoService(cliente, new CatalogoProxy(catalogoRemoto, cliente.Plan),
                             repo, new ConsoleLogger());

    public static ExportService CrearExportService() => new ExportService();
}
