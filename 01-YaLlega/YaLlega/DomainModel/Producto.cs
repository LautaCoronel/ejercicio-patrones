namespace DomainModel;

public class Producto : IExportable
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public Plan PlanMinimo { get; set; }   // plan necesario para poder pedirlo

    public string Accept(IExportVisitor v) => v.Visit(this);
}
