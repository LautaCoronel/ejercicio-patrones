using System;

namespace Patrones.Estructurales.Decorator;

public abstract class Bebida
{
    public abstract double Costo();
    public abstract string Descripcion();
}
public class Cafe : Bebida
{
    public override double Costo() => 10;
    public override string Descripcion() => "Cafe";
}

// Decorator base
public abstract class CondimentoDecorator : Bebida
{
    protected readonly Bebida _bebida;
    protected CondimentoDecorator(Bebida b) { _bebida = b; }
}
public class Leche : CondimentoDecorator
{
    public Leche(Bebida b) : base(b) { }
    public override double Costo() => _bebida.Costo() + 2;
    public override string Descripcion() => _bebida.Descripcion() + " + leche";
}
public class Azucar : CondimentoDecorator
{
    public Azucar(Bebida b) : base(b) { }
    public override double Costo() => _bebida.Costo() + 0.5;
    public override string Descripcion() => _bebida.Descripcion() + " + azucar";
}

class Program
{
    static void Main()
    {
        Bebida pedido = new Azucar(new Leche(new Cafe()));
        System.Console.WriteLine($"{pedido.Descripcion()} = {pedido.Costo()}"); // 12.5
    }
}
