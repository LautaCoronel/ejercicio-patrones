using DomainModel.Entities;

namespace DomainModel.Contracts
{
    /// <summary>
    /// Observador del patron Observer: recibe el aviso de cambio de la bateria.
    /// </summary>
    public interface IObservador
    {
        void Actualizar(Bateria bateria);
    }
}
