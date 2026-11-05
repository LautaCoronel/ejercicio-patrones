using System;
using System.Threading.Tasks;

namespace Ejercicios
{
    class Program
    {
        static async Task Main()
        {
            Ejercicio1.Correr();
            Ejercicio2.Correr();
            Ejercicio3.Correr();
            Ejercicio4.Correr();
            await Ejercicio5.Correr();

            Console.WriteLine();
            Console.WriteLine("Fin de los ejercicios");
        }
    }
}
