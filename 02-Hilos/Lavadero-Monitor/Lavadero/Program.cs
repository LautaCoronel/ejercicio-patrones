using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Lavadero
{
    class Program
    {
        private static readonly Stopwatch reloj = Stopwatch.StartNew();
        private static readonly object candadoConsola = new object();

        public static void Log(string mensaje)
        {
            lock (candadoConsola)
                Console.WriteLine($"{reloj.Elapsed.TotalSeconds,5:0.00}s  {mensaje}");
        }

        static void Main()
        {
            const int AUTOS_POR_RECEPCIONISTA = 5;
            PlayaDeEspera playa = new PlayaDeEspera(4);

            Recepcionista[] recepcionistas =
            {
                new Recepcionista("Recep. Ana  ", playa, AUTOS_POR_RECEPCIONISTA, 1),
                new Recepcionista("Recep. Beto ", playa, AUTOS_POR_RECEPCIONISTA, 2),
                new Recepcionista("Recep. Carla", playa, AUTOS_POR_RECEPCIONISTA, 3)
            };
            Box[] boxes =
            {
                new Box("Box Dani", playa, 4),
                new Box("Box Eli ", playa, 5)
            };

            List<Thread> hilosRecep = new List<Thread>();
            List<Thread> hilosBoxes = new List<Thread>();
            foreach (Recepcionista r in recepcionistas) hilosRecep.Add(new Thread(r.Ejecutar));
            foreach (Box b in boxes) hilosBoxes.Add(new Thread(b.Ejecutar));

            hilosRecep.ForEach(h => h.Start());
            hilosBoxes.ForEach(h => h.Start());

            hilosRecep.ForEach(h => h.Join());      //esperamos a los recepcionistas...
            playa.Cerrar();                         //...y cerramos la playa
            hilosBoxes.ForEach(h => h.Join());

            int total = 0;
            foreach (Box b in boxes) total += b.Lavados;
            int esperado = recepcionistas.Length * AUTOS_POR_RECEPCIONISTA;
            Console.WriteLine();
            Console.WriteLine($"Autos: {esperado} | Autos lavados: {total} | " +
                              (total == esperado ? "OK" : "ERROR: se perdieron autos"));
        }
    }
}
