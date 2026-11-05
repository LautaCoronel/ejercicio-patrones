using System;

namespace SistemaImpresion
{
    //FACTORY: crea el notificador segun el tipo que pidio el usuario.
    //Asi la impresora (consumidor) no hace "new" de ninguna clase concreta.
    static class NotificadorFactory
    {
        public static INotificador Crear(TipoNotificacion tipo)
        {
            switch (tipo)
            {
                case TipoNotificacion.SMS: return new NotificadorSMS();
                case TipoNotificacion.WhatsApp: return new NotificadorWhatsApp();
                case TipoNotificacion.Email: return new NotificadorEmail();
                default: throw new ArgumentException("Tipo de notificacion desconocido: " + tipo);
            }
        }
    }
}
