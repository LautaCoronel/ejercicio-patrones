using DomainModel;
using System.Collections.Generic;

namespace BLL.Patrones;

public interface IMenu { string Consultar(Producto p); }

// RealSubject: trae el menu del servidor remoto.
public class MenuRemoto : IMenu
{
    public string Consultar(Producto p) => "descargado del servidor: " + p.Nombre;
}

// Proxy: controla el plan (proteccion) y cachea los ultimos 5 (virtual).
public class MenuProxy : IMenu
{
    private readonly MenuRemoto _real = new MenuRemoto();
    private readonly Plan _plan;
    private readonly LinkedList<string> _cache = new LinkedList<string>();

    public MenuProxy(Plan plan) { _plan = plan; }

    public string Consultar(Producto p)
    {
        if (p.EsPremium && _plan != Plan.Premium)
            return "BLOQUEADO: tu plan no incluye " + p.Nombre;

        if (_cache.Contains(p.Nombre))
            return "desde cache: " + p.Nombre;

        var r = _real.Consultar(p);
        _cache.AddFirst(p.Nombre);
        if (_cache.Count > 5) _cache.RemoveLast();
        return r;
    }
}
