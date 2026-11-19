using System;
using System.IO;
using System.Text;
using DAL.Contracts;

namespace DAL.Implementations
{
    /// <summary>
    /// Variante: ROTACION POR TAMANO. Cuando bitacora.txt supera el maximo
    /// se renombra a bitacora_yyyy-MM-dd_HHmmss.txt y se empieza uno nuevo.
    /// </summary>
    public class BitacoraRepositoryRotacionTamano : IBitacoraRepository
    {
        private readonly string _carpeta;
        private readonly long _maxBytes;

        public BitacoraRepositoryRotacionTamano(string carpeta = null, long maxBytes = 1024)
        {
            _carpeta = carpeta ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitacoras");
            _maxBytes = maxBytes;
        }

        public void Escribir(string linea)
        {
            try
            {
                if (!Directory.Exists(_carpeta))
                    Directory.CreateDirectory(_carpeta);

                string ruta = Path.Combine(_carpeta, "bitacora.txt");
                if (File.Exists(ruta) && new FileInfo(ruta).Length >= _maxBytes)
                {
                    string baseNombre = "bitacora_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                    string destino = Path.Combine(_carpeta, baseNombre + ".txt");
                    int n = 1;
                    while (File.Exists(destino))        // evita pisar uno del mismo segundo
                        destino = Path.Combine(_carpeta, baseNombre + "_" + n++ + ".txt");
                    File.Move(ruta, destino);
                }

                File.AppendAllText(ruta, linea + Environment.NewLine, Encoding.UTF8);
            }
            catch (IOException ex)
            {
                Console.WriteLine("[ERROR BITACORA] " + ex.Message);
            }
        }
    }
}
