using System;
using System.Threading;
using System.Threading.Tasks;

namespace Practica.Subprocesamiento
{
    // Guarda las cajas y la plata que se junta.
    // Es lo unico que comparten todos los clientes.
    class Supermercado
    {
        private readonly SemaphoreSlim cajas;
        private readonly object candado = new object();
        private int recaudado;
        private int cobros;

        public Supermercado(int cantidadDeCajas)
        {
            cajas = new SemaphoreSlim(cantidadDeCajas);
        }

        // El cliente pide una caja. Si estan todas ocupadas, espera aca.
        public async Task EsperarCaja()
        {
            await cajas.WaitAsync();
        }

        // Cuando termina de pagar, suelta la caja para el que sigue.
        public void LiberarCaja()
        {
            cajas.Release();
        }

        // Sumo la plata. El lock hace que no se pisen dos cobros a la vez.
        public void Cobrar(string nombre, int monto)
        {
            lock (candado)
            {
                recaudado = recaudado + monto;
                cobros = cobros + 1;
                Console.WriteLine(nombre + " pago $" + monto + "  (total $" + recaudado + ")");
            }
        }

        public int Recaudado
        {
            get { return recaudado; }
        }

        public int Cobros
        {
            get { return cobros; }
        }
    }
}
