using System;
using System.Collections.Generic;
using System.Threading;

namespace SistemaImpresion
{
    //SINGLETON: una sola impresora compartida en toda la aplicacion.
    //PRODUCTOR-CONSUMIDOR: los usuarios dejan trabajos en la cola (productores)
    //y la impresora los saca y los imprime de a uno (consumidor).
    sealed class Impresora
    {
        private static readonly Impresora instancia = new Impresora();
        public static Impresora Instancia { get { return instancia; } }

        private readonly Queue<TrabajoImpresion> trabajos = new Queue<TrabajoImpresion>();
        private readonly object candado = new object();
        private int capacidad = 3;
        private bool cerrada = false;
        private readonly Random rnd = new Random(99);

        //Para las pruebas: orden de llegada vs. orden de impresion.
        public List<int> Recibidos { get; private set; } = new List<int>();
        public List<int> Impresos { get; private set; } = new List<int>();
        public int MaximaOcupacion { get; private set; }
        public int Notificaciones { get; private set; }

        private Impresora() { }     //constructor privado: nadie mas puede crearla

        //Deja la impresora lista para una nueva tanda de trabajos.
        public void Iniciar(int capacidad)
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);
                this.capacidad = capacidad;
                cerrada = false;
                trabajos.Clear();
                Recibidos = new List<int>();
                Impresos = new List<int>();
                MaximaOcupacion = 0;
                Notificaciones = 0;
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //Productor: la llama un USUARIO.
        //CONTROL DE VELOCIDAD: si la cola esta llena, el usuario espera
        //hasta que la impresora le avise (PulseAll) que hay lugar.
        public void EnviarTrabajo(TrabajoImpresion t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));

            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                if (cerrada)
                    throw new InvalidOperationException("La impresora esta cerrada, no acepta trabajos");

                //WHILE y no IF: al despertar hay que volver a verificar.
                while (trabajos.Count == capacidad)
                {
                    Program.Log($"{t.Usuario} espera: cola llena " + Ocupacion());
                    Monitor.Wait(candado);          //libera el lock y duerme
                }

                trabajos.Enqueue(t);
                Recibidos.Add(t.Numero);
                if (trabajos.Count > MaximaOcupacion) MaximaOcupacion = trabajos.Count;
                Program.Log($"{t.Usuario} envia {t}  " + Ocupacion());

                Monitor.PulseAll(candado);          //aviso a la impresora
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //Consumidor: saca el siguiente trabajo en orden de llegada (FIFO).
        //Devuelve false cuando esta cerrada y vacia (no hay mas trabajo).
        public bool ObtenerSiguienteTrabajo(out TrabajoImpresion t)
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                while (trabajos.Count == 0 && !cerrada)
                    Monitor.Wait(candado);          //espero a que un usuario envie

                if (trabajos.Count == 0)            //vacia y cerrada
                {
                    t = null;
                    return false;
                }

                t = trabajos.Dequeue();
                Monitor.PulseAll(candado);          //ahora hay lugar: aviso a los usuarios
                return true;
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //Simula la impresion y notifica al usuario por el medio que eligio.
        public void Imprimir(TrabajoImpresion t)
        {
            if (t == null)
                throw new InvalidOperationException("No hay trabajos en la cola para imprimir");

            int demora;
            lock (rnd) demora = rnd.Next(300, 700);
            Thread.Sleep(demora);                   //imprimir FUERA del lock de la cola

            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);
                Impresos.Add(t.Numero);
                Notificaciones++;
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }

            Program.Log($"Impresora imprimio {t} de {t.Usuario.Trim()}");

            //La impresora no sabe que clase concreta notifica ni como lo hace.
            INotificador notificador = NotificadorFactory.Crear(t.Notificacion);
            notificador.Notificar(t.Usuario, $"tu trabajo {t} ya se imprimio");
        }

        //Hilo de la impresora: procesa trabajos hasta que se cierra y se vacia.
        public void Ejecutar()
        {
            TrabajoImpresion t;
            while (ObtenerSiguienteTrabajo(out t))
            {
                try
                {
                    Imprimir(t);
                }
                catch (Exception ex)
                {
                    Program.Log("Error al imprimir " + t + ": " + ex.Message);
                }
            }
            Program.Log($"Impresora se apaga: imprimio {Impresos.Count} trabajos");
        }

        //La llama Main cuando todos los usuarios terminaron.
        public void Cerrar()
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);
                cerrada = true;
                Monitor.PulseAll(candado);          //despierta a la impresora si duerme
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        //Se llama siempre con el lock tomado.
        private string Ocupacion()
        {
            return "[" + new string('#', trabajos.Count)
                       + new string('.', capacidad - trabajos.Count) + "]";
        }
    }
}
