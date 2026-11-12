using System;
using System.Collections.Generic;

namespace Patrones.Estructurales.Proxy;

public interface IContenido
{
    string Reproducir(string titulo);
}

// RealSubject: descarga del servidor remoto
public class ContenidoRemoto : IContenido
{
    public string Reproducir(string titulo) => "descargado del servidor: " + titulo;
}

// Proxy: controla plan + cachea los ultimos 5
public class ContenidoProxy : IContenido
{
    private readonly ContenidoRemoto _real = new ContenidoRemoto();
    private readonly string _plan;
    private readonly LinkedList<string> _cache = new LinkedList<string>();

    public ContenidoProxy(string plan) { _plan = plan; }

    public string Reproducir(string titulo)
    {
        if (_plan == "basico" && titulo.Contains("4K"))
            return "BLOQUEADO: tu plan no incluye " + titulo;   // proteccion

        if (_cache.Contains(titulo))
            return "desde cache: " + titulo;                   // virtual / cache

        var r = _real.Reproducir(titulo);                      // delega al real
        _cache.AddFirst(titulo);
        if (_cache.Count > 5) _cache.RemoveLast();             // mantiene 5
        return r;
    }
}

class Program
{
    static void Main()
    {
        IContenido c = new ContenidoProxy("basico");
        System.Console.WriteLine(c.Reproducir("Serie"));    // descargado...
        System.Console.WriteLine(c.Reproducir("Serie"));    // desde cache
        System.Console.WriteLine(c.Reproducir("Peli 4K"));  // BLOQUEADO
    }
}
