using DomainModel.Entities;

namespace DomainModel.Contracts
{
  
    public interface IObservador
    {

        void Actualizar(Bateria bateria);
    }
}
