using System;
using System.Collections.Generic;
using DomainModel.Contracts;

namespace DomainModel.Entities
{
    /// <summary>
    /// Sujeto concreto del patron Observer.
    /// Modela la bateria de una laptop y avisa a todos sus suscriptores
    /// (relacion 1 a muchos) cada vez que se modifica Conectado.
    /// </summary>
    public class Bateria : ISujeto
    {
        // Lista de observadores suscriptos (1 bateria -> muchos suscriptores).
        private readonly List<IObservador> _observadores = new List<IObservador>();

        private bool _conectado;
        private int _carga;

        // Minutos que tarda una carga de 0 a 100% y que dura una bateria al 100%.
        private const int MinutosCargaCompleta = 120;
        private const int MinutosUsoCompleto = 300;

        /// <summary>¿La bateria esta conectada a la tension? Al cambiarlo se notifica.</summary>
        public bool Conectado
        {
            get { return _conectado; }
            set
            {
                _conectado = value;
                RecalcularTiempos();
                Notificar();            // notificacion automatica al cambiar Conectado
            }
        }

        /// <summary>Porcentaje de carga (0 a 100).</summary>
        public int Carga
        {
            get { return _carga; }
            set
            {
                ValidarCarga(value);
                _carga = value;
                RecalcularTiempos();
            }
        }

        /// <summary>Minutos restantes de carga (0 si esta completa o desconectada).</summary>
        public int TiempoCarga { get; private set; }

        /// <summary>Minutos restantes de uso (0 si esta conectada).</summary>
        public int TiempoUso { get; private set; }

        public Bateria(bool conectado, int carga)
        {
            ValidarCarga(carga);
            _carga = carga;
            _conectado = conectado;
            RecalcularTiempos();
        }

        // Validacion de rango: la carga solo puede ir de 0 a 100.
        private static void ValidarCarga(int carga)
        {
            if (carga < 0 || carga > 100)
                throw new ArgumentOutOfRangeException(nameof(carga),
                    "La carga debe estar entre 0 y 100 por ciento.");
        }

        // Conectada: calcula lo que falta para el 100%.
        // Desconectada: calcula cuanto dura con la carga actual.
        private void RecalcularTiempos()
        {
            if (_conectado)
            {
                TiempoCarga = (int)Math.Round((100 - _carga) * MinutosCargaCompleta / 100.0);
                TiempoUso = 0;
            }
            else
            {
                TiempoCarga = 0;
                TiempoUso = (int)Math.Round(_carga * MinutosUsoCompleto / 100.0);
            }
        }

        /// <summary>Agrega un suscriptor (no permite repetidos ni null).</summary>
        public void Suscribir(IObservador observador)
        {
            if (observador == null)
                throw new ArgumentNullException(nameof(observador));
            if (!_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        /// <summary>Quita un suscriptor: deja de recibir avisos.</summary>
        public void Desuscribir(IObservador observador)
        {
            if (observador != null)
                _observadores.Remove(observador);
        }

        /// <summary>
        /// Avisa el cambio de estado a todos los suscriptores.
        /// Si uno falla, se informa el error y se sigue con los demas.
        /// </summary>
        public void Notificar()
        {
            // Se recorre una copia por si alguien se desuscribe durante el aviso.
            foreach (IObservador observador in _observadores.ToArray())
            {
                try
                {
                    observador.Actualizar(this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] El suscriptor {observador.GetType().Name} fallo: {ex.Message}");
                }
            }
        }
    }
}
