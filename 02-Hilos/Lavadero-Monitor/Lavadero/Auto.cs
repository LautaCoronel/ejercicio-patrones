namespace Lavadero
{
    class Auto
    {
        public int Numero;
        public string Patente;
        public string Modelo;
        public string Recepcionista;

        public override string ToString()
        {
            return $"#{Numero} {Patente}: {Modelo}";
        }
    }
}
