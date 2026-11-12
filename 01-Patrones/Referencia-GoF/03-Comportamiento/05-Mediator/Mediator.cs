using System;
using System.Collections.Generic;

namespace Patrones.Comportamiento.Mediator;

// Mediator
public interface IMediador { void Enviar(string msg, Usuario de); }

public class SalaChat : IMediador
{
    private readonly List<Usuario> _usuarios = new List<Usuario>();
    public void Registrar(Usuario u) { _usuarios.Add(u); u.Sala = this; }
    public void Enviar(string msg, Usuario de)
    {
        foreach (var u in _usuarios)
            if (u != de) u.Recibir(msg);
    }
}

// Colleague
public class Usuario
{
    public string Nombre { get; }
    public IMediador Sala { get; set; }
    public Usuario(string n) { Nombre = n; }
    public void Enviar(string msg) => Sala.Enviar($"{Nombre}: {msg}", this);
    public void Recibir(string msg) => System.Console.WriteLine($"[{Nombre}] {msg}");
}

class Program
{
    static void Main()
    {
        var sala = new SalaChat();
        var a = new Usuario("Ana"); var b = new Usuario("Beto");
        sala.Registrar(a); sala.Registrar(b);
        a.Enviar("hola"); // Beto recibe
    }
}
