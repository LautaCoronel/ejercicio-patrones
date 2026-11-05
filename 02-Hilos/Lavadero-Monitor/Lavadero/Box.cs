using System;
using System.Threading;

namespace Lavadero
{
    class Box
    {
        private readonly string nombre;
        private readonly PlayaDeEspera playa;
        private readonly Random rnd;
        public int Lavados { get; private set; }

        public Box(string nombre, PlayaDeEspera playa, int semilla)
        {
            this.nombre = nombre;
            this.playa = playa;
            rnd = new Random(semilla);
        }

        public void Ejecutar()
        {
            Auto a;
            while (playa.Tomar(nombre, out a))
            {
                Thread.Sleep(rnd.Next(500, 1200));  //lavar FUERA del lock
                Lavados++;
                Program.Log($"{nombre} termino {a} (de {a.Recepcionista})");
            }
            Program.Log($"{nombre} se va: lavo {Lavados} autos");
        }
    }
}
