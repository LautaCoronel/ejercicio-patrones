using System;
using System.Collections.Generic;
using DomainModel.Contracts;

namespace DomainModel.Entities
{
    
    public class Bateria : ISujeto
    {
        
        private readonly List<IObservador> _observadores = new List<IObservador>();

        private bool _conectado;
        private int _carga;

        public bool Conectado
        {
            get { return _conectado; }
            set
            {
                _conectado = value;
                RecalcularTiempos();
                Notificar(); 
            }
        }

      
        public int Carga
        {
            get { return _carga; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(Carga),
                        "La carga debe estar entre 0 y 100 por ciento.");

                _carga = value;
                RecalcularTiempos();
            }
        }

       
        public int TiempoCarga { get; private set; }

   
        public int TiempoUso { get; private set; }

        
        private const int MinutosCargaCompleta = 120; 
        private const int MinutosUsoCompleto = 300;    

        public Bateria(bool conectado, int carga)
        {
            if (carga < 0 || carga > 100)
                throw new ArgumentOutOfRangeException(nameof(carga),
                    "La carga debe estar entre 0 y 100 por ciento.");

            _carga = carga;
            _conectado = conectado;
            RecalcularTiempos();
        }

        
        private void RecalcularTiempos()
        {
            if (_conectado)
            {
               
                int porcentajeFaltante = 100 - _carga;
                TiempoCarga = (int)Math.Round(porcentajeFaltante * MinutosCargaCompleta / 100.0);
                TiempoUso = 0; 
            }
            else
            {
             
                TiempoCarga = 0;
                TiempoUso = (int)Math.Round(_carga * MinutosUsoCompleto / 100.0);
            }
        }

        

        public void Suscribir(IObservador observador)
        {
            if (observador != null && !_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        public void Desuscribir(IObservador observador)
        {
            if (observador != null)
                _observadores.Remove(observador);
        }

       
        public void Notificar()
        {
            foreach (var observador in _observadores)
            {
                observador.Actualizar(this);
            }
        }
    }
}
