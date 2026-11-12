using System;
using System.Collections.Generic;

namespace Patrones.Estructurales.Composite;

public abstract class ComponenteCatalogo
{
    public string Nombre { get; set; }
    public abstract void Mostrar(string sangria);
}
public class Contenido : ComponenteCatalogo // Leaf
{
    public override void Mostrar(string sangria) =>
        System.Console.WriteLine(sangria + Nombre);
}
public class Categoria : ComponenteCatalogo // Composite
{
    private readonly List<ComponenteCatalogo> _hijos = new List<ComponenteCatalogo>();
    public void Agregar(ComponenteCatalogo c) => _hijos.Add(c);
    public override void Mostrar(string sangria)
    {
        System.Console.WriteLine(sangria + "[" + Nombre + "]");
        foreach (var h in _hijos) h.Mostrar(sangria + "  ");
    }
}

class Program
{
    static void Main()
    {
        var raiz = new Categoria { Nombre = "Inicio" };
        var series = new Categoria { Nombre = "Series" };
        series.Agregar(new Contenido { Nombre = "Serie A" });
        raiz.Agregar(series);
        raiz.Agregar(new Contenido { Nombre = "Pelicula X" });
        raiz.Mostrar("");
    }
}
