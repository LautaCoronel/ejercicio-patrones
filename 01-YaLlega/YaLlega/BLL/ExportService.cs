using BLL.Patrones;
using DomainModel;
using System.Collections.Generic;
using System.Text;

namespace BLL;

// Exporta cualquier listado con una unica rutina (Visitor).
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
