using System;
using System.Threading.Tasks;

namespace Practica.Subprocesamiento
{
    // Un cliente que llega, espera su caja, paga y se va.
    class Cliente
    {
        private readonly string nombre;
        private readonly int monto;
        private readonly Supermercado super;

        public Cliente(string nombre, int monto, Supermercado super)
        {
            this.nombre = nombre;
            this.monto = monto;
            this.super = super;
        }

        public async Task Pagar()
        {
            Console.WriteLine(nombre + " llega con $" + monto);

            // Si las 3 cajas estan ocupadas, aca se queda esperando.
            await super.EsperarCaja();

            try
            {
                Console.WriteLine(nombre + " esta pagando");
                await Task.Delay(500);      // medio segundo cobrando
                super.Cobrar(nombre, monto);
            }
            finally
            {
                super.LiberarCaja();        // la caja se suelta siempre
            }
        }
    }
}
