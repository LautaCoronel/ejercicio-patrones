namespace DomainModel;

public class Cliente : IExportable
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Plan Plan { get; set; }
    public string Accept(IExportVisitor v) => v.Visit(this);
}
