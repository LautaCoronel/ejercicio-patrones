using System;
using System.Collections.Generic;
using DomainModel;
using BLL;

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
            Console.WriteLine(ex);
        }
        Console.WriteLine();
        Console.WriteLine("Fin. Presione una tecla para cerrar...");
        Console.ReadKey();
    }

    static void Run()
    {
        Console.WriteLine("=== YaLlega (Strategy + Proxy + Visitor + Repository) ===");
        var cliente = new Cliente { Id = 1, Nombre = "Lautaro", Plan = Plan.Clasico };
        var pizza = new Producto { Id = 1, Nombre = "Pizza", EsPremium = false };
        var sushi = new Producto { Id = 2, Nombre = "Sushi Premium", EsPremium = true };

        var pedidos = AppFactory.CrearPedidoService(cliente.Plan);
        Console.WriteLine(pedidos.Agregar(cliente, pizza, 1000m, 10));  // tarifa normal
        Console.WriteLine(pedidos.Agregar(cliente, pizza, 1000m, 90));  // desde cache, +60%
        Console.WriteLine(pedidos.Agregar(cliente, sushi, 1000m, 50));  // BLOQUEADO (plan clasico)
        pedidos.SetModoPromocion(true);
        Console.WriteLine(pedidos.Agregar(cliente, pizza, 1000m, 90));  // gratis (modo promocion)
        Console.WriteLine("Pedidos guardados: " + pedidos.Historial().Count);

        Console.WriteLine();
        Console.WriteLine("-- Exportar listado (Visitor) --");
        var export = AppFactory.CrearExportService();
        var listado = new List<IExportable> { cliente, pizza, sushi };
        Console.Write(export.Exportar(listado));
    }
}
