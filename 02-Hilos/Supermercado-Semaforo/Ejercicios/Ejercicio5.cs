using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ejercicios
{
    // Tres tareas que esperan. Con WhenAll tardan lo que la mas lenta.
    class Ejercicio5
    {
        public static async Task Correr()
        {
            Console.WriteLine();
            Console.WriteLine("5) async y await");

            Stopwatch reloj = Stopwatch.StartNew();

            Task cafe = Preparar("Cafe", 2000);
            Task tostada = Preparar("Tostadas", 3000);
            Task huevos = Preparar("Huevos", 2500);

            await Task.WhenAll(cafe, tostada, huevos);
            reloj.Stop();

            Console.WriteLine("Tardo " + reloj.ElapsedMilliseconds + " ms (no 7500)");
        }

        static async Task Preparar(string nombre, int ms)
        {
            Console.WriteLine("Empiezo: " + nombre);
            await Task.Delay(ms);       // espera sin bloquear el hilo
            Console.WriteLine("Termine: " + nombre);
        }
    }
}
