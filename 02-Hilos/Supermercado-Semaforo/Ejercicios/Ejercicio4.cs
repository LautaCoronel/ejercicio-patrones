using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ejercicios
{
    // El mismo trabajo con un for normal y con Parallel.For.
    class Ejercicio4
    {
        public static void Correr()
        {
            Console.WriteLine();
            Console.WriteLine("4) for contra Parallel.For");

            const int N = 40;

            Stopwatch reloj = Stopwatch.StartNew();
            for (int i = 0; i < N; i++)
            {
                Trabajo(i);
            }
            reloj.Stop();
            Console.WriteLine("For normal:   " + reloj.ElapsedMilliseconds + " ms");

            reloj.Restart();
            Parallel.For(0, N, i => Trabajo(i));
            reloj.Stop();
            Console.WriteLine("Parallel.For: " + reloj.ElapsedMilliseconds + " ms");
            Console.WriteLine("Nucleos: " + Environment.ProcessorCount);
        }

        static void Trabajo(int n)
        {
            double x = 0;
            for (int k = 0; k < 3000000; k++)
            {
                x = x + Math.Sqrt(k + n);
            }
        }
    }
}
