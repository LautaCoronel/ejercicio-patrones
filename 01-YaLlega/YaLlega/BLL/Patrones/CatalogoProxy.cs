using System;
using System.Collections.Generic;
using DomainModel;

namespace BLL.Patrones;

// Subject: lo unico que conoce la pantalla de armado del pedido.
public interface ICatalogo
{
    Producto ObtenerProducto(int id);
}

// RealSubject: el catalogo alojado en el servidor remoto.
public class CatalogoRemoto : ICatalogo
{
    private readonly Dictionary<int, Producto> _productos = new Dictionary<int, Producto>();
    public int Descargas { get; private set; }     // cuantas veces se bajo algo del servidor

    public void Cargar(Producto p) => _productos[p.Id] = p;

    public Producto ObtenerProducto(int id)
    {
        if (!_productos.ContainsKey(id))
            throw new KeyNotFoundException("No existe el producto " + id);
        Descargas++;
        return _productos[id];
    }
}

// Se lanza cuando el producto no esta incluido en el plan del cliente.
public class ProductoNoPermitidoException : Exception
{
    public ProductoNoPermitidoException(string mensaje) : base(mensaje) { }
}

// PROXY:
//  - de proteccion: impide pedir productos que no estan en el plan del cliente.
//  - virtual/cache: guarda en el dispositivo los ultimos 5 menus consultados.
// La pantalla usa ICatalogo y no sabe si el menu vino del servidor o del dispositivo.
public class CatalogoProxy : ICatalogo
{
    private const int MaxCache = 5;
    private readonly ICatalogo _real;
    private readonly Plan _plan;
    private readonly LinkedList<Producto> _cache = new LinkedList<Producto>();

    public string UltimoOrigen { get; private set; }   // solo para registrar en el pedido

    public CatalogoProxy(ICatalogo real, Plan plan)
    {
        _real = real;
        _plan = plan;
    }

    public Producto ObtenerProducto(int id)
    {
        Producto p = BuscarEnCache(id);
        if (p != null)
        {
            UltimoOrigen = "DISPOSITIVO";
            _cache.Remove(p);
            _cache.AddFirst(p);            // pasa a ser el mas reciente
        }
        else
        {
            p = _real.ObtenerProducto(id);
            UltimoOrigen = "SERVIDOR";
            _cache.AddFirst(p);
            if (_cache.Count > MaxCache) _cache.RemoveLast();   // saca el mas viejo
        }

        if (p.PlanMinimo > _plan)
            throw new ProductoNoPermitidoException(
                $"{p.Nombre} requiere plan {p.PlanMinimo} y el cliente tiene {_plan}");

        return p;
    }

    private Producto BuscarEnCache(int id)
    {
        foreach (var p in _cache)
            if (p.Id == id) return p;
        return null;
    }
}
