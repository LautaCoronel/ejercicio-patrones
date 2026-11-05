namespace SistemaImpresion
{
    //STRATEGY: cada medio de notificacion es un algoritmo intercambiable.
    //La impresora solo conoce esta interfaz, no las clases concretas.
    interface INotificador
    {
        void Notificar(string usuario, string mensaje);
    }
}
