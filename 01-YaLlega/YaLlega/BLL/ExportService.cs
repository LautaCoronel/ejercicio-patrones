using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BLL.Patrones;
using DomainModel;

namespace BLL;

// Unica rutina de exportacion para cualquier listado (Visitor).
public class ExportService
{
    public string ArmarTexto(IEnumerable<IExportable> items)
    {
        var lista = items.ToList();
        var sb = new StringBuilder();
        if (lista.Count == 0) return "";

        sb.AppendLine(lista[0].Accept(new EncabezadoVisitor()));
        var fila = new FilaTextoVisitor();
        foreach (var it in lista) sb.AppendLine(it.Accept(fila));
        return sb.ToString();
    }

    public void Exportar(IEnumerable<IExportable> items, string ruta)
        => File.WriteAllText(ruta, ArmarTexto(items), Encoding.UTF8);
}
