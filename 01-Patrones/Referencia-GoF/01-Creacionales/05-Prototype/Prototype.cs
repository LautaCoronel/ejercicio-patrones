using System;

namespace Patrones.Creacionales.Prototype;

public abstract class Figura
{
    public int X { get; set; }
    public int Y { get; set; }
    public abstract Figura Clonar();
}
public class Circulo : Figura
{
    public int Radio { get; set; }
    public override Figura Clonar() => (Figura)this.MemberwiseClone();
}
public class Rectangulo : Figura
{
    public int Ancho { get; set; }
    public int Alto { get; set; }
    public override Figura Clonar() => (Figura)this.MemberwiseClone();
}

class Program
{
    static void Main()
    {
        var c1 = new Circulo { X = 1, Y = 2, Radio = 5 };
        var c2 = (Circulo)c1.Clonar(); // copia sin usar new Circulo(...)
        c2.X = 99;
        System.Console.WriteLine($"{c1.X} / {c2.X}"); // 1 / 99
    }
}
