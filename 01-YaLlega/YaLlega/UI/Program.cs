using System;
using System.Collections.Generic;
using BLL;
using BLL.Patrones;
using DAL;
using DomainModel;

namespace UI;

class Program
{
    static void Main()
    {
        try
        {
            Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("### ERROR EN TIEMPO DE EJECUCION ###");
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine();
        Console.WriteLine("Fin. Presione una tecla para cerrar...");
        Console.ReadKey();
    }

    static void Run()
    {
        Console.WriteLine("=== YaLlega S.A. (Strategy + Proxy + Visitor + Repository) ===");

        var catalogo = AppFactory.CrearCatalogo();
        var repo = new RepositorioPedidosArchivo("pedidos.txt");
        var ana = new Cliente { Id = 1, Nombre = "Ana", Plan = Plan.Clasico };

        var servicio = AppFactory.CrearPedidoService(ana, catalogo, repo);

        Console.WriteLine("\n-- Pedido 1: cambia la demanda mientras esta abierto --");
        servicio.AbrirPedido();                       // demanda baja: tarifa normal
        servicio.AgregarProducto(1);                  // Pizza desde el SERVIDOR
        servicio.MedirDemanda(NivelDemanda.Media);    // +25%
        servicio.AgregarProducto(4);                  // Sushi: RECHAZADO (plan clasico)
        servicio.AgregarProducto(1);                  // Pizza desde el DISPOSITIVO (cache)
        servicio.MedirDemanda(NivelDemanda.Alta);     // +60% (hora pico, lluvia)
        servicio.CerrarPedido();

        Console.WriteLine("\n-- Pedido 2: el operador fuerza modo promocion --");
        servicio.AbrirPedido();
        servicio.AgregarProducto(2);
        servicio.ForzarModo(new EnvioPromocion());    // boton del operador
        servicio.MedirDemanda(NivelDemanda.Alta);     // no cambia: el modo esta forzado
        servicio.CerrarPedido();

        Console.WriteLine("\nPedidos guardados en pedidos.txt");

        Console.WriteLine("\n-- Exportar listados (Visitor) --");
        var export = AppFactory.CrearExportService();
        var productos = new List<IExportable> { catalogo.ObtenerProducto(1), catalogo.ObtenerProducto(4) };
        var clientes = new List<IExportable> { ana };
        var pedidos = new List<IExportable>(repo.ObtenerTodos());

        export.Exportar(productos, "productos.txt");
        export.Exportar(clientes, "clientes.txt");
        export.Exportar(pedidos, "pedidos_export.txt");
        Console.Write(export.ArmarTexto(pedidos));
        Console.WriteLine("Exportados: productos.txt, clientes.txt, pedidos_export.txt");
    }
}
