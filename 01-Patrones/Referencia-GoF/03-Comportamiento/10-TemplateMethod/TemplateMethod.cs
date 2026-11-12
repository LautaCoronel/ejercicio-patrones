using System;
using System.Text;

namespace Patrones.Comportamiento.TemplateMethod;

// AbstractClass
public abstract class Exportador
{
    // Template Method: esqueleto fijo
    public string Exportar()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Encabezado());
        sb.AppendLine(Cuerpo());
        return sb.ToString();
    }
    protected abstract string Encabezado();
    protected abstract string Cuerpo();
}

public class ExportadorCsv : Exportador
{
    protected override string Encabezado() => "id,nombre";
    protected override string Cuerpo() => "1,Ana";
}
public class ExportadorTexto : Exportador
{
    protected override string Encabezado() => "=== Listado ===";
    protected override string Cuerpo() => "Ana";
}

class Program
{
    static void Main()
    {
        Exportador e = new ExportadorCsv();
        System.Console.WriteLine(e.Exportar());
    }
}
