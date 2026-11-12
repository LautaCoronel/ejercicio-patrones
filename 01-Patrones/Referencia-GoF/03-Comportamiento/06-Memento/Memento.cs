using System;
using System.Collections.Generic;

namespace Patrones.Comportamiento.Memento;

// Memento
public class Memento
{
    public int Posicion { get; }
    public Memento(int pos) { Posicion = pos; }
}

// Originator
public class Reproductor
{
    public int Posicion { get; set; }
    public Memento Guardar() => new Memento(Posicion);
    public void Restaurar(Memento m) => Posicion = m.Posicion;
}

// Caretaker
public class Historial
{
    private readonly Stack<Memento> _estados = new Stack<Memento>();
    public void Push(Memento m) => _estados.Push(m);
    public Memento Pop() => _estados.Pop();
}

class Program
{
    static void Main()
    {
        var r = new Reproductor { Posicion = 100 };
        var h = new Historial();
        h.Push(r.Guardar());  // guarda 100
        r.Posicion = 500;     // avanza
        r.Restaurar(h.Pop()); // vuelve a 100
        System.Console.WriteLine(r.Posicion); // 100
    }
}
