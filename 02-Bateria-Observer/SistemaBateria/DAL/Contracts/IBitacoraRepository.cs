namespace DAL.Contracts
{
    /// <summary>
    /// Contrato de persistencia de la bitacora. La BLL depende de esta
    /// interfaz y no de como se guarda (archivo, base de datos, etc.).
    /// </summary>
    public interface IBitacoraRepository
    {
        void Escribir(string linea);
    }
}
