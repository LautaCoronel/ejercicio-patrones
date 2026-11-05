namespace SistemaImpresion
{
    class NotificadorSMS : INotificador
    {
        public void Notificar(string usuario, string mensaje)
        {
            Program.Log($"   [SMS] a {usuario.Trim()}: {mensaje}");
        }
    }
}
