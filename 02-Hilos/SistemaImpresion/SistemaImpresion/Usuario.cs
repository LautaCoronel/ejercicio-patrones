using System;
using System.Threading;

namespace SistemaImpresion
{
    class Usuario
    {
        private static readonly string[] Documentos =
            { "Informe.pdf", "Factura.docx", "Planilla.xlsx", "Contrato.pdf", "Foto.png", "Nota.txt" };
        private static int ultimoNumero = 0;

        private readonly string nombre;
        private readonly TipoNotificacion notificacion;
        private readonly Random rnd;
        public int Enviados { get; private set; }

        public Usuario(string nombre, TipoNotificacion notificacion, int semilla)
        {
            this.nombre = nombre;
            this.notificacion = notificacion;
            rnd = new Random(semilla);      //una semilla distinta por usuario
        }

        public void Ejecutar()
        {
            int cantidad = rnd.Next(2, 6);  //numero aleatorio de trabajos
            for (int i = 0; i < cantidad; i++)
            {
                Thread.Sleep(rnd.Next(100, 400));   //tiempo aleatorio entre envios
                TrabajoImpresion t = new TrabajoImpresion
                {
                    Numero = Interlocked.Increment(ref ultimoNumero),
                    Documento = Documentos[rnd.Next(Documentos.Length)],
                    Usuario = nombre,
                    Notificacion = notificacion
                };
                Impresora.Instancia.EnviarTrabajo(t);
                Enviados++;
            }
            Program.Log($"{nombre} termino: envio {Enviados} trabajos");
        }
    }
}
