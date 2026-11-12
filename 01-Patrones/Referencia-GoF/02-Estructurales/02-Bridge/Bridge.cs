using System;

namespace Patrones.Estructurales.Bridge;

// Implementor
public interface IDispositivo
{
    void Encender();
    void SetVolumen(int v);
}
public class TV : IDispositivo
{
    public void Encender() => System.Console.WriteLine("TV ON");
    public void SetVolumen(int v) => System.Console.WriteLine("TV vol " + v);
}
public class Radio : IDispositivo
{
    public void Encender() => System.Console.WriteLine("Radio ON");
    public void SetVolumen(int v) => System.Console.WriteLine("Radio vol " + v);
}

// Abstraction
public class Control
{
    protected readonly IDispositivo _d;
    public Control(IDispositivo d) { _d = d; }
    public virtual void Prender() => _d.Encender();
}
public class ControlAvanzado : Control
{
    public ControlAvanzado(IDispositivo d) : base(d) { }
    public void Silenciar() => _d.SetVolumen(0);
}

class Program
{
    static void Main()
    {
        var c = new ControlAvanzado(new TV());
        c.Prender();
        c.Silenciar();
    }
}
