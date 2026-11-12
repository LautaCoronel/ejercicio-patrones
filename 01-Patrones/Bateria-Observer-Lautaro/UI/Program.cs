using System;
using BLL.Suscriptores;
using DAL.Contracts;
using DAL.Implementations;
using DomainModel.Entities;

namespace UI
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== SISTEMA DE MONITOREO DE BATERÍA (PATRÓN OBSERVER) ===");

            try
            {
                
                Bateria bateria = new Bateria(conectado: false, carga: 50);

                
                IBitacoraRepository repositorio = new BitacoraRepositoryArchivo();
                var visual = new SuscriptorVisual();
                var bitacora = new SuscriptorBitacora(repositorio);

                
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

              
                Console.WriteLine("\n>> Se desuscribe la bitácora...");
                bateria.Desuscribir(bitacora);

                Console.WriteLine("\n>> La carga baja al 40% y se reconecta...");
                bateria.Carga = 40;
                bateria.Conectado = true;
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
