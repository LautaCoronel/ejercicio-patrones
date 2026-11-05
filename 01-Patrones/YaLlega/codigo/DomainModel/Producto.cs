namespace DomainModel;

public class Producto : IExportable
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public bool EsPremium { get; set; }
    public string Accept(IExportVisitor v) => v.Visit(this);
}
