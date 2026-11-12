using System;

namespace Patrones.Comportamiento.Interpreter;

public interface IExpresion { int Interpretar(); }

// Terminal
public class Numero : IExpresion
{
    private readonly int _valor;
    public Numero(int v) { _valor = v; }
    public int Interpretar() => _valor;
}

// No terminales
public class Suma : IExpresion
{
    private readonly IExpresion _izq, _der;
    public Suma(IExpresion i, IExpresion d) { _izq = i; _der = d; }
    public int Interpretar() => _izq.Interpretar() + _der.Interpretar();
}
public class Resta : IExpresion
{
    private readonly IExpresion _izq, _der;
    public Resta(IExpresion i, IExpresion d) { _izq = i; _der = d; }
    public int Interpretar() => _izq.Interpretar() - _der.Interpretar();
}

class Program
{
    static void Main()
    {
        // "uno mas cinco menos cuatro" => (1 + 5) - 4
        IExpresion exp = new Resta(new Suma(new Numero(1), new Numero(5)), new Numero(4));
        System.Console.WriteLine(exp.Interpretar()); // 2
    }
}
