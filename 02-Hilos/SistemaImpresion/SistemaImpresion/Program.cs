using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SistemaImpresion
{
    class Program
    {
        private static readonly Stopwatch reloj = Stopwatch.StartNew();
        private static readonly object candadoConsola = new object();
        public static bool Silencioso = false;     //las pruebas no ensucian la consola

        public static void Log(string mensaje)
        {
            if (Silencioso) return;
            lock (candadoConsola)
                Console.WriteLine($"{reloj.Elapsed.TotalSeconds,5:0.00}s  {mensaje}");
        }

        static void Main()
        {
            Impresora impresora = Impresora.Instancia;   //Singleton
            impresora.Iniciar(3);                        //cola de 3 trabajos como maximo

            Usuario[] usuarios =
            {
                new Usuario("Ana  ", TipoNotificacion.SMS, 1),
                new Usuario("Beto ", TipoNotificacion.WhatsApp, 2),
                new Usuario("Carla", TipoNotificacion.Email, 3),
                new Usuario("Dani ", TipoNotificacion.WhatsApp, 4)
            };

            List<Thread> hilosUsuarios = new List<Thread>();
            foreach (Usuario u in usuarios) hilosUsuarios.Add(new Thread(u.Ejecutar));
            Thread hiloImpresora = new Thread(impresora.Ejecutar);

            hiloImpresora.Start();
            hilosUsuarios.ForEach(h => h.Start());

            hilosUsuarios.ForEach(h => h.Join());   //esperamos a los usuarios...
            impresora.Cerrar();                     //...y cerramos la impresora
            hiloImpresora.Join();

            int enviados = 0;
            foreach (Usuario u in usuarios) enviados += u.Enviados;
            int impresos = impresora.Impresos.Count;
            Console.WriteLine();
            Console.WriteLine($"Enviados: {enviados} | Impresos: {impresos} | " +
                              (enviados == impresos ? "OK" : "ERROR: se perdieron trabajos"));

            Pruebas.Ejecutar();

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para cerrar...");
            Console.ReadKey();
        }
    }
}
