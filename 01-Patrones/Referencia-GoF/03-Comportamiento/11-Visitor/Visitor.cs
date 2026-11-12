using System;

namespace Patrones.Comportamiento.Visitor;

// Visitor
public interface IExportVisitor
{
    string Visit(Contenido c);
    string Visit(Usuario u);
}

// Element
public interface IExportable { string Accept(IExportVisitor v); }

public class Contenido : IExportable
{
    public string Titulo { get; set; }
    public string Accept(IExportVisitor v) => v.Visit(this);
}
public class Usuario : IExportable
{
    public string Nombre { get; set; }
    public string Accept(IExportVisitor v) => v.Visit(this);
}

// ConcreteVisitor: una unica rutina de exportacion a texto
public class TextoVisitor : IExportVisitor
{
    public string Visit(Contenido c) => "CONTENIDO;" + c.Titulo;
    public string Visit(Usuario u) => "USUARIO;" + u.Nombre;
}

class Program
{
    static void Main()
    {
        var items = new IExportable[]
        {
            new Contenido { Titulo = "Serie A" },
            new Usuario { Nombre = "Ana" }
        };
        var exportar = new TextoVisitor();
        foreach (var it in items)
            System.Console.WriteLine(it.Accept(exportar));
    }
}
