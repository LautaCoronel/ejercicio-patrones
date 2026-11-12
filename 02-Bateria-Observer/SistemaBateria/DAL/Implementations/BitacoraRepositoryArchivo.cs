using System;
using System.IO;
using System.Text;
using DAL.Contracts;

namespace DAL.Implementations
{
    /// <summary>
    /// Guarda la bitacora en archivos de texto con ROTACION POR FECHA:
    /// un archivo por dia (bitacora_yyyy-MM-dd.txt) dentro de la carpeta Bitacoras.
    /// Si el archivo no existe lo crea; si existe, agrega la linea al final.
    /// </summary>
    public class BitacoraRepositoryArchivo : IBitacoraRepository
    {
        private readonly string _carpeta;

        // Por defecto usa la carpeta Bitacoras junto al .exe.
        public BitacoraRepositoryArchivo(string carpeta = null)
        {
            _carpeta = carpeta ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitacoras");
        }

        // Nombre del archivo segun la fecha de hoy (rotacion diaria).
        private string ObtenerRutaDelDia()
        {
            string nombreArchivo = $"bitacora_{DateTime.Now:yyyy-MM-dd}.txt";
            return Path.Combine(_carpeta, nombreArchivo);
        }

        public void Escribir(string linea)
        {
            try
            {
                // Crea la carpeta la primera vez.
                if (!Directory.Exists(_carpeta))
                    Directory.CreateDirectory(_carpeta);

                string ruta = ObtenerRutaDelDia();

                // AppendAllText crea el archivo o agrega al final si ya existe.
                File.AppendAllText(ruta, linea + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // Un error de disco no debe cortar el programa.
                Console.WriteLine($"[ERROR BITÁCORA] No se pudo escribir en el archivo: {ex.Message}");
            }
        }
    }
}
