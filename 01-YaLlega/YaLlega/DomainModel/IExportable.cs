namespace DomainModel;

// Elemento del Visitor: cualquier clase que se quiera exportar implementa esto.
public interface IExportable
{
    string Accept(IExportVisitor v);
}
