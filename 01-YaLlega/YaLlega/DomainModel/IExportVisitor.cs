namespace DomainModel;

// Visitor: una operacion por cada tipo exportable.
// Para exportar una clase nueva se agrega un Visit aca y en los visitors.
public interface IExportVisitor
{
    string Visit(Producto p);
    string Visit(Cliente c);
    string Visit(Pedido p);
}
