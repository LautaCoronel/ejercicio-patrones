using System;
using System.Collections.Generic;

namespace Patrones.Comportamiento.Observer;

// Observer
public interface IObservador { void Actualizar(string contenido); }

// Subject
public class Canal
{
    private readonly List<IObservador> _subs = new List<IObservador>();
    public void Suscribir(IObservador o) => _subs.Add(o);
    public void Desuscribir(IObservador o) => _subs.Remove(o);
    public void Publicar(string contenido)
    {
        foreach (var o in _subs) o.Actualizar(contenido);
    }
}

public class UsuarioSuscriptor : IObservador
{
    private readonly string _nombre;
    public UsuarioSuscriptor(string n) { _nombre = n; }
    public void Actualizar(string c) => System.Console.WriteLine($"{_nombre} recibio: {c}");
}

class Program
{
    static void Main()
    {
        var canal = new Canal();
        canal.Suscribir(new UsuarioSuscriptor("Ana"));
        canal.Suscribir(new UsuarioSuscriptor("Beto"));
        canal.Publicar("Nuevo episodio"); // ambos reciben
    }
}
