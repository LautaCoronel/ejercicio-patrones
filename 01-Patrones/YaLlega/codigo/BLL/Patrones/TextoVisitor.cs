using DomainModel;

namespace BLL.Patrones;

// ConcreteVisitor: exporta cualquier elemento a una linea de texto.
public class TextoVisitor : IExportVisitor
{
    public string Visit(Producto p) => "PRODUCTO;" + p.Nombre + ";premium=" + p.EsPremium;
    public string Visit(Cliente c) => "CLIENTE;" + c.Nombre + ";plan=" + c.Plan;
}
