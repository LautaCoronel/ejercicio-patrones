using System;
using System.Threading;

namespace Ejercicios
{
    // Dos hilos sumando al mismo contador. Sin lock se pierden sumas.
    class Ejercicio2
    {
        static int contador;
        static readonly object candado = new object();

        public static void Correr()
        {
            Console.WriteLine();
            Console.WriteLine("2) Condicion de carrera");

            // Primero sin lock, para ver el error.
            contador = 0;
            Thread a = new Thread(SumarSinLock);
            Thread b = new Thread(SumarSinLock);
            a.Start();
            b.Start();
            a.Join();
            b.Join();
            Console.WriteLine("Sin lock: esperaba 2000000 y dio " + contador);

            // Ahora con lock.
            contador = 0;
            Thread c = new Thread(SumarConLock);
            Thread d = new Thread(SumarConLock);
            c.Start();
            d.Start();
            c.Join();
            d.Join();
            Console.WriteLine("Con lock: esperaba 2000000 y dio " + contador);
        }

        static void SumarSinLock()
        {
            for (int i = 0; i < 1000000; i++)
            {
                contador = contador + 1;   // leer, sumar y escribir: no es de una sola vez
            }
        }

        static void SumarConLock()
        {
            for (int i = 0; i < 1000000; i++)
            {
                lock (candado)
                {
                    contador = contador + 1;
                }
            }
        }
    }
}
