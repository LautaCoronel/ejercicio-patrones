namespace DomainModel;

// Visitor: una operacion por cada tipo de elemento exportable.
public interface IExportVisitor
{
    string Visit(Producto p);
    string Visit(Cliente c);
}
