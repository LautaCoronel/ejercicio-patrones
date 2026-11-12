using System;
using System.IO;
using System.Text;
using DAL.Contracts;

namespace DAL.Implementations
{
    
    public class BitacoraRepositoryArchivo : IBitacoraRepository
    {
        private readonly string _carpeta;

        public BitacoraRepositoryArchivo(string carpeta = null)
        {
           
            _carpeta = carpeta ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitacoras");
        }

        
        private string ObtenerRutaDelDia()
        {
            string nombreArchivo = $"bitacora_{DateTime.Now:yyyy-MM-dd}.txt";
            return Path.Combine(_carpeta, nombreArchivo);
        }

        public void Escribir(string linea)
        {
            try
            {
    
                if (!Directory.Exists(_carpeta))
                    Directory.CreateDirectory(_carpeta);

                string ruta = ObtenerRutaDelDia();

                
                File.AppendAllText(ruta, linea + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"[ERROR BITÁCORA] No se pudo escribir en el archivo: {ex.Message}");
            }
        }
    }
}
