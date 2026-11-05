namespace SistemaImpresion
{
    class NotificadorEmail : INotificador
    {
        public void Notificar(string usuario, string mensaje)
        {
            Program.Log($"   [Email] a {usuario.Trim()}: {mensaje}");
        }
    }
}
