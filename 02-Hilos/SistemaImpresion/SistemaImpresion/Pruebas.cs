using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SistemaImpresion
{
    //Pruebas del sistema en condiciones de concurrencia.
    static class Pruebas
    {
        private static int ok = 0, fallas = 0;

        public static void Ejecutar()
        {
            Console.WriteLine();
            Console.WriteLine("========== PRUEBAS ==========");
            Program.Silencioso = true;

            Verificar("1. Singleton: siempre la misma impresora",
                ReferenceEquals(Impresora.Instancia, Impresora.Instancia));

            Verificar("2. Factory: crea el notificador correcto",
                NotificadorFactory.Crear(TipoNotificacion.SMS) is NotificadorSMS &&
                NotificadorFactory.Crear(TipoNotificacion.WhatsApp) is NotificadorWhatsApp &&
                NotificadorFactory.Crear(TipoNotificacion.Email) is NotificadorEmail);

            //Tanda grande: 10 usuarios contra una cola de 2.
            const int CAPACIDAD = 2;
            int enviados = Simular(10, CAPACIDAD);
            Impresora imp = Impresora.Instancia;

            Verificar("3. No se pierden trabajos (enviados = impresos)",
                enviados == imp.Impresos.Count && imp.Recibidos.Count == enviados);
            Verificar("4. Se imprimen en el orden en que se recibieron",
                imp.Recibidos.SequenceEqual(imp.Impresos));
            Verificar("5. Control de velocidad: la cola nunca supera la capacidad",
                imp.MaximaOcupacion <= CAPACIDAD);
            Verificar("6. Ningun trabajo se imprime dos veces",
                imp.Impresos.Distinct().Count() == imp.Impresos.Count);
            Verificar("7. Se notifica una vez por cada trabajo impreso",
                imp.Notificaciones == imp.Impresos.Count);

            Verificar("8. Excepcion al imprimir sin trabajo",
                Lanza<InvalidOperationException>(() => imp.Imprimir(null)));

            TrabajoImpresion t;
            Verificar("9. Cola cerrada y vacia: ObtenerSiguienteTrabajo devuelve false",
                !imp.ObtenerSiguienteTrabajo(out t) && t == null);

            Verificar("10. Excepcion al enviar a una impresora cerrada",
                Lanza<InvalidOperationException>(() =>
                    imp.EnviarTrabajo(new TrabajoImpresion { Numero = 0, Usuario = "X" })));

            Program.Silencioso = false;
            Console.WriteLine();
            Console.WriteLine($"Pruebas: {ok} OK | {fallas} con falla");
        }

        //Corre usuarios + impresora en hilos y devuelve cuantos trabajos se enviaron.
        private static int Simular(int cantidadUsuarios, int capacidad)
        {
            Impresora.Instancia.Iniciar(capacidad);
            List<Usuario> usuarios = new List<Usuario>();
            List<Thread> hilos = new List<Thread>();
            for (int i = 0; i < cantidadUsuarios; i++)
            {
                Usuario u = new Usuario("Prueba" + i, (TipoNotificacion)(i % 3), 100 + i);
                usuarios.Add(u);
                hilos.Add(new Thread(u.Ejecutar));
            }
            Thread hiloImpresora = new Thread(Impresora.Instancia.Ejecutar);

            hiloImpresora.Start();
            hilos.ForEach(h => h.Start());
            hilos.ForEach(h => h.Join());
            Impresora.Instancia.Cerrar();
            hiloImpresora.Join();

            return usuarios.Sum(u => u.Enviados);
        }

        private static bool Lanza<T>(Action accion) where T : Exception
        {
            try { accion(); return false; }
            catch (T) { return true; }
        }

        private static void Verificar(string nombre, bool resultado)
        {
            if (resultado) ok++; else fallas++;
            Console.WriteLine((resultado ? "[OK]    " : "[FALLA] ") + nombre);
        }
    }
}
