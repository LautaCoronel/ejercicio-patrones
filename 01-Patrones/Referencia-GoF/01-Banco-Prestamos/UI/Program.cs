using System;
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
        Console.WriteLine("=== BANCO - Aprobacion de prestamos (Chain of Responsibility) ===");
        var service = AppFactory.CrearPrestamoService();
        var cliente = new Cliente { Id = 1, Nombre = "Franco" };
        foreach (var monto in new decimal[] { 50000m, 300000m, 800000m, 2000000m })
        {
            var p = service.Solicitar(cliente, monto);
            Console.WriteLine($"Monto {monto}: aprobado={p.Aprobado} por {p.AprobadoPor}");
        }
        Console.WriteLine("Guardados en DAL: " + service.Historial().Count);
    }
}
