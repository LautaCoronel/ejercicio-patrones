using System;
using DomainModel.Entities;

namespace BLL.Suscriptores
{
    /// <summary>
    /// Formatos personalizados para la linea de la bitacora.
    /// En SuscriptorBitacora.Actualizar se reemplaza la linea por:
    ///   string linea = FormatoBitacora.Csv(bateria);   (o Texto / Json)
    /// </summary>
    public static class FormatoBitacora
    {
        public const string EncabezadoCsv = "FechaHora;Estado;Carga;TiempoCarga;TiempoUso";

        private static string FechaHora()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private static string Estado(Bateria b)
        {
            return b.Conectado ? "CARGANDO" : "DESCONECTADO";
        }

        // [2026-11-19 16:00:00] CARGANDO | Carga: 50% | Faltan 60 min
        public static string Texto(Bateria b)
        {
            string tiempo = b.Conectado
                ? (b.Carga >= 100 ? "Carga completa" : "Faltan " + b.TiempoCarga + " min")
                : "Autonomia " + b.TiempoUso + " min";
            return "[" + FechaHora() + "] " + Estado(b) + " | Carga: " + b.Carga + "% | " + tiempo;
        }

        // 2026-11-19 16:00:00;CARGANDO;50;60;0
        public static string Csv(Bateria b)
        {
            return FechaHora() + ";" + Estado(b) + ";" + b.Carga + ";" + b.TiempoCarga + ";" + b.TiempoUso;
        }

        // {"fechaHora":"2026-11-19 16:00:00","estado":"CARGANDO","carga":50,"tiempoCarga":60,"tiempoUso":0}
        public static string Json(Bateria b)
        {
            return "{\"fechaHora\":\"" + FechaHora() + "\",\"estado\":\"" + Estado(b) +
                   "\",\"carga\":" + b.Carga + ",\"tiempoCarga\":" + b.TiempoCarga +
                   ",\"tiempoUso\":" + b.TiempoUso + "}";
        }
    }
}
