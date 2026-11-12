namespace DomainModel.Contracts
{
 
    public interface ISujeto
    {
        void Suscribir(IObservador observador);
        void Desuscribir(IObservador observador);
        void Notificar();
    }
}
