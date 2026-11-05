namespace DomainModel;

public interface IExportable
{
    string Accept(IExportVisitor v);
}
