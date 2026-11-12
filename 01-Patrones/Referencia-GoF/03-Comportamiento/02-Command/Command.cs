using System;
using System.Collections.Generic;

namespace Patrones.Comportamiento.Command;

// Receiver
public class Reproductor
{
    public void Play() => System.Console.WriteLine("Play");
    public void Pause() => System.Console.WriteLine("Pause");
}

// Command
public interface IComando { void Ejecutar(); void Deshacer(); }

public class ComandoPlay : IComando
{
    private readonly Reproductor _r;
    public ComandoPlay(Reproductor r) { _r = r; }
    public void Ejecutar() => _r.Play();
    public void Deshacer() => _r.Pause();
}

// Invoker
public class Control
{
    private readonly Stack<IComando> _historial = new Stack<IComando>();
    public void Presionar(IComando c) { c.Ejecutar(); _historial.Push(c); }
    public void Deshacer() { if (_historial.Count > 0) _historial.Pop().Deshacer(); }
}

class Program
{
    static void Main()
    {
        var control = new Control();
        var r = new Reproductor();
        control.Presionar(new ComandoPlay(r)); // Play
        control.Deshacer();                    // Pause
    }
}
