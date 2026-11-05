using System;
using System.Threading;

namespace Lavadero
{
    class Recepcionista
    {
        private static readonly string[] Modelos =
            { "Gol", "Corsa", "Hilux", "Onix", "Cronos", "Amarok", "Focus" };
        private static int ultimoNumero = 0;

        private readonly string nombre;
        private readonly PlayaDeEspera playa;
        private readonly int cantidad;
        private readonly Random rnd;

        public Recepcionista(string nombre, PlayaDeEspera playa, int cantidad, int semilla)
        {
            this.nombre = nombre;
            this.playa = playa;
            this.cantidad = cantidad;
            rnd = new Random(semilla);   //una semilla distinta por recepcionista
        }

        public void Ejecutar()
        {
            for (int i = 0; i < cantidad; i++)
            {
                Thread.Sleep(rnd.Next(200, 600));   //recibir el auto en la entrada
                Auto a = new Auto
                {
                    Numero = Interlocked.Increment(ref ultimoNumero),
                    Patente = Patente(),
                    Modelo = Modelos[rnd.Next(Modelos.Length)],
                    Recepcionista = nombre
                };
                playa.Dejar(a);
            }
            Program.Log($"{nombre} termino su turno");
        }

        private string Patente()
        {
            return (char)('A' + rnd.Next(26)) + rnd.Next(100, 1000).ToString()
                 + (char)('A' + rnd.Next(26)) + (char)('A' + rnd.Next(26));
        }
    }
}
