using System;
using BLL.Suscriptores;
using DAL.Contracts;
using DAL.Implementations;
using DomainModel.Entities;

namespace UI
{
    /// <summary>
    /// Prueba del diseno: crea la bateria, suscribe el visual y la bitacora
    /// y simula cambios de estado.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== SISTEMA DE MONITOREO DE BATERÍA (PATRÓN OBSERVER) ===");

            try
            {
                // Sujeto: bateria desconectada al 50%.
                Bateria bateria = new Bateria(conectado: false, carga: 50);

                // Observadores: consola y bitacora en archivo.
                IBitacoraRepository repositorio = new BitacoraRepositoryArchivo();
                var visual = new SuscriptorVisual();
                var bitacora = new SuscriptorBitacora(repositorio);

                // Relacion 1 a muchos: una bateria, dos suscriptores.
                bateria.Suscribir(visual);
                bateria.Suscribir(bitacora);

                Console.WriteLine("\n>> Se conecta el cargador...");
                bateria.Conectado = true;

                Console.WriteLine("\n>> La carga sube al 85%...");
                bateria.Carga = 85;
                bateria.Conectado = true; 

                Console.WriteLine("\n>> La batería llega al 100%...");
                bateria.Carga = 100;
                bateria.Conectado = true;

                Console.WriteLine("\n>> Se desconecta el cargador...");
                bateria.Conectado = false;

                // Desde aca la bitacora ya no recibe avisos.
                Console.WriteLine("\n>> Se desuscribe la bitácora...");
                bateria.Desuscribir(bitacora);

                Console.WriteLine("\n>> La carga baja al 40% y se reconecta...");
                bateria.Carga = 40;
                bateria.Conectado = true;

                // Validacion de rango: una carga fuera de 0-100 lanza excepcion.
                Console.WriteLine("\n>> Se intenta poner la carga en 150%...");
                bateria.Carga = 150;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR]: {ex.Message}");
            }

            Console.WriteLine("\nProceso finalizado. Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}
