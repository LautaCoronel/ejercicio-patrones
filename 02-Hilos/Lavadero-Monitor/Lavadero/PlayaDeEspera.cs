using System.Collections.Generic;
using System.Threading;

namespace Lavadero
{
    class PlayaDeEspera
    {
        private readonly Queue<Auto> autos = new Queue<Auto>();
        private readonly object candado = new object();
        private readonly int capacidad;
        private bool cerrada = false;

        public PlayaDeEspera(int capacidad) { this.capacidad = capacidad; }

        //Productor: la llama un RECEPCIONISTA.
        public void Dejar(Auto a)
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                //WHILE y no IF: al despertar hay que volver a verificar,
                //otro recepcionista pudo haber ocupado el lugar antes que yo.
                while (autos.Count == capacidad)
                {
                    Program.Log($"{a.Recepcionista} espera: playa llena " + Ocupacion());
                    Monitor.Wait(candado);          //libera el lock y duerme
                }

                autos.Enqueue(a); //Dejar el auto en la playa...
                Program.Log($"{a.Recepcionista} deja {a}  " + Ocupacion());

                //PulseAll y no Pulse: en la cola de espera hay recepcionistas Y
                //boxes; con Pulse podria despertar al tipo equivocado.
                Monitor.PulseAll(candado);
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //Consumidor: la llama un BOX.
        //Devuelve false cuando esta cerrada y vacia (no hay mas trabajo).
        public bool Tomar(string box, out Auto a)
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                while (autos.Count == 0 && !cerrada)
                    Monitor.Wait(candado);//Espero a que un recepcionista haga PulseAll

                if (autos.Count == 0)               //vacia y cerrada
                {
                    a = null;
                    return false;
                }

                a = autos.Dequeue(); //Tomo el auto de la playa...
                Program.Log($"{box} toma {a}  " + Ocupacion());
                Monitor.PulseAll(candado);          //ahora hay lugar
                return true;
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //La llama Main cuando todos los recepcionistas terminaron.
        public void Cerrar()
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);
                cerrada = true;
                Monitor.PulseAll(candado);          //despierta a boxes dormidos
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //Se llama siempre con el lock tomado.
        private string Ocupacion()
        {
            return "[" + new string('#', autos.Count)
                       + new string('.', capacidad - autos.Count) + "]";
        }
    }
}
