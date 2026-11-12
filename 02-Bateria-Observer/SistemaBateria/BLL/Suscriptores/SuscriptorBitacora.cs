using System;
using DAL.Contracts;
using DomainModel.Contracts;
using DomainModel.Entities;

namespace BLL.Suscriptores
{
    /// <summary>
    /// Observador concreto (Segundo Parcial).
    /// Ante cada cambio de estado de la batería arma un registro con
    /// fecha/hora y lo persiste a través del repositorio, que se encarga
    /// de la rotación diaria de archivos.
    /// </summary>
    public class SuscriptorBitacora : IObservador
    {
        private readonly IBitacoraRepository _repositorio;

        // Recibe el repositorio por constructor (bajo acoplamiento:
        // la bitácora no sabe si escribe en archivo, base de datos, etc.).
        public SuscriptorBitacora(IBitacoraRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void Actualizar(Bateria bateria)
        {
            // Timestamp de cada evento, como pide la consigna.
            string fechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string estado = bateria.Conectado ? "CARGANDO" : "DESCONECTADO";

            // Según el estado, registramos el tiempo relevante (carga o uso).
            string detalleTiempo = bateria.Conectado
                ? (bateria.Carga >= 100
                    ? "Carga completa"
                    : $"Faltan {bateria.TiempoCarga} min para completar")
                : $"Autonomía restante: {bateria.TiempoUso} min";

            // Formato personalizado de la línea de bitácora.
            string linea = $"[{fechaHora}] Estado: {estado} | Carga: {bateria.Carga}% | {detalleTiempo}";

            _repositorio.Escribir(linea);

            // Confirmación visual mínima de que se registró.
            Console.WriteLine("   -> [Bitácora] Evento registrado en archivo.");
        }
    }
}
