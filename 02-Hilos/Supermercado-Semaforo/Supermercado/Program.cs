using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Practica.Subprocesamiento
{
    class Program
    {
        static async Task Main()
        {
            // Los montos los armo una sola vez, asi las 3 pruebas dan lo mismo.
            int[] montos = new int[10];
            Random azar = new Random();
            for (int i = 0; i < montos.Length; i++)
            {
                montos[i] = azar.Next(1000, 10000);
            }

            await Probar(1, montos);
            await Probar(3, montos);
            await Probar(10, montos);
        }

        // Abre el super con esa cantidad de cajas y manda a los 10 clientes juntos.
        static async Task Probar(int cantidadDeCajas, int[] montos)
        {
            Console.WriteLine();
            Console.WriteLine("Con " + cantidadDeCajas + " caja(s):");

            Supermercado super = new Supermercado(cantidadDeCajas);
            List<Task> clientes = new List<Task>();
            Stopwatch reloj = Stopwatch.StartNew();

            for (int i = 0; i < montos.Length; i++)
            {
                Cliente c = new Cliente("Cliente " + (i + 1), montos[i], super);
                clientes.Add(c.Pagar());
            }

            await Task.WhenAll(clientes);   // espero a que terminen todos
            reloj.Stop();

            Console.WriteLine("Recaudado: $" + super.Recaudado + " en " + super.Cobros + " cobros");
            Console.WriteLine("Tardo " + reloj.ElapsedMilliseconds + " ms");
        }
    }
}
