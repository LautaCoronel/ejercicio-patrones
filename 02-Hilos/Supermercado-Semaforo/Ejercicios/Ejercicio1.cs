using System;
using System.Threading;

namespace Ejercicios
{
    // Dos hilos escribiendo al mismo tiempo. El orden cambia en cada corrida.
    class Ejercicio1
    {
        public static void Correr()
        {
            Console.WriteLine();
            Console.WriteLine("1) Dos hilos");

            Thread a = new Thread(() => Escribir("A"));
            Thread b = new Thread(() => Escribir("B"));

            a.Start();
            b.Start();

            a.Join();   // espero a que termine A
            b.Join();   // y a que termine B

            Console.WriteLine();
            Console.WriteLine("Terminaron los dos");
        }

        static void Escribir(string letra)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write(letra);
                Thread.Sleep(10);   // le doy tiempo al otro hilo
            }
        }
    }
}
