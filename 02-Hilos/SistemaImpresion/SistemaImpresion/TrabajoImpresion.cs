namespace SistemaImpresion
{
    class TrabajoImpresion
    {
        public int Numero;
        public string Documento;
        public string Usuario;
        public TipoNotificacion Notificacion;

        public override string ToString()
        {
            return $"#{Numero} {Documento}";
        }
    }
}
