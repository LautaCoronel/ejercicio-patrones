namespace SistemaImpresion
{
    class NotificadorWhatsApp : INotificador
    {
        public void Notificar(string usuario, string mensaje)
        {
            Program.Log($"   [WhatsApp] a {usuario.Trim()}: {mensaje}");
        }
    }
}
