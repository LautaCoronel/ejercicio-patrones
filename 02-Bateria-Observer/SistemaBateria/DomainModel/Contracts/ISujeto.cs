namespace DomainModel.Contracts
{
    /// <summary>
    /// Sujeto del patron Observer: mantiene la lista de suscriptores
    /// y les avisa cuando cambia su estado.
    /// </summary>
    public interface ISujeto
    {
        void Suscribir(IObservador observador);
        void Desuscribir(IObservador observador);
        void Notificar();
    }
}
