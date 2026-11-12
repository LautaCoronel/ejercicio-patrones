using System;
using DomainModel.Contracts;
using DomainModel.Entities;

namespace BLL.Suscriptores
{
    /// <summary>
    /// Observador concreto (Primer Parcial).
    /// Muestra por consola el estado de la batería cada vez que cambia:
    /// conexión, porcentaje y tiempo de carga o de uso según corresponda.
    /// </summary>
    public class SuscriptorVisual : IObservador
    {
        public void Actualizar(Bateria bateria)
        {
            Console.WriteLine();
            Console.WriteLine("========= SUSCRIPTOR VISUAL =========");

            if (bateria.Conectado)
            {
                Console.WriteLine("Estado: CARGANDO (conectada a la tensión)");
                Console.WriteLine($"Carga actual: {bateria.Carga}%");

                if (bateria.Carga >= 100)
                    Console.WriteLine("La batería está completamente cargada.");
                else
                    Console.WriteLine($"Tiempo restante de carga: {bateria.TiempoCarga} min");
            }
            else
            {
                Console.WriteLine("Estado: DESCONECTADO (usando batería)");
                Console.WriteLine($"Carga actual: {bateria.Carga}%");
                Console.WriteLine($"Tiempo estimado de uso: {bateria.TiempoUso} min");
            }

            Console.WriteLine("=====================================");
        }
    }
}
