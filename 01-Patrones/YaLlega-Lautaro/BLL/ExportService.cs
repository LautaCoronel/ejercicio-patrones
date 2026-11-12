using BLL.Patrones;
using DomainModel;
using System.Collections.Generic;
using System.Text;

namespace BLL;


public class ExportService
{
    public string Exportar(List<IExportable> items)
    {
        var v = new TextoVisitor();
        var sb = new StringBuilder();
        foreach (var it in items) sb.AppendLine(it.Accept(v));
        return sb.ToString();
    }
}
