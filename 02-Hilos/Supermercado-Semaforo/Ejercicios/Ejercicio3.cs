using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicios
{
    // Un ascensor para 3. Seis personas quieren subir.
    class Ejercicio3
    {
        static SemaphoreSlim ascensor = new SemaphoreSlim(3);

        public static void Correr()
        {
            Console.WriteLine();
            Console.WriteLine("3) Semaforo (ascensor para 3)");

            Task[] personas = new Task[6];
            for (int i = 0; i < personas.Length; i++)
            {
                int id = i + 1;
                personas[i] = Task.Run(() => Subir(id));
            }

            Task.WaitAll(personas);   // bloqueo simple: espero a todas
            Console.WriteLine("Todos subieron y bajaron");
        }

        static void Subir(int id)
        {
            Console.WriteLine("Persona " + id + " espera");
            ascensor.Wait();          // si ya hay 3 arriba, espera aca
            Console.WriteLine("Persona " + id + " sube");
            Thread.Sleep(1000);
            Console.WriteLine("Persona " + id + " baja");
            ascensor.Release();       // libera el lugar
        }
    }
}
