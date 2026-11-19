using System;
using System.IO;
using System.Text;
using DAL.Contracts;

namespace DAL.Implementations
{
    /// <summary>
    /// Bitacora en "bitacora.txt" con ROTACION POR FECHA:
    /// si el archivo es de un dia anterior, se renombra a bitacora_yyyy-MM-dd.txt
    /// (con la fecha de ese dia) y se empieza un bitacora.txt nuevo.
    /// Opcional: un encabezado que se escribe al crear el archivo (util para CSV).
    /// </summary>
    public class BitacoraRepositoryRotacionDiaria : IBitacoraRepository
    {
        private readonly string _carpeta;
        private readonly string _encabezado;

        public BitacoraRepositoryRotacionDiaria(string carpeta = null, string encabezado = null)
        {
            _carpeta = carpeta ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitacoras");
            _encabezado = encabezado;
        }

        public string RutaActual
        {
            get { return Path.Combine(_carpeta, "bitacora.txt"); }
        }

        public void Escribir(string linea)
        {
            try
            {
                if (!Directory.Exists(_carpeta))
                    Directory.CreateDirectory(_carpeta);

                RotarSiCambioElDia();

                // Archivo nuevo: primero el encabezado (si hay).
                if (!File.Exists(RutaActual) && _encabezado != null)
                    File.AppendAllText(RutaActual, _encabezado + Environment.NewLine, Encoding.UTF8);

                File.AppendAllText(RutaActual, linea + Environment.NewLine, Encoding.UTF8);
            }
            catch (IOException ex)
            {
                Console.WriteLine("[ERROR BITACORA] " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("[ERROR BITACORA] Sin permisos: " + ex.Message);
            }
        }

        // Si bitacora.txt se escribio por ultima vez otro dia, se archiva con esa fecha.
        private void RotarSiCambioElDia()
        {
            if (!File.Exists(RutaActual)) return;

            DateTime fechaArchivo = File.GetLastWriteTime(RutaActual).Date;
            if (fechaArchivo == DateTime.Today) return;

            string destino = Path.Combine(_carpeta, "bitacora_" + fechaArchivo.ToString("yyyy-MM-dd") + ".txt");
            if (File.Exists(destino))
            {
                // Ya habia uno de esa fecha: se agrega al final y se borra el actual.
                File.AppendAllText(destino, File.ReadAllText(RutaActual), Encoding.UTF8);
                File.Delete(RutaActual);
            }
            else
            {
                File.Move(RutaActual, destino);
            }
        }
    }
}
