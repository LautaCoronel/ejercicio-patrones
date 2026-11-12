namespace DomainModel;


public interface IExportVisitor
{
    string Visit(Producto p);
    string Visit(Cliente c);
}
