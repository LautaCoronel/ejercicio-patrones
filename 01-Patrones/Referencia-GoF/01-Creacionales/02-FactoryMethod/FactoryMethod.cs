using System;

namespace Patrones.Creacionales.FactoryMethod;

// Product
public abstract class Boleto
{
    public abstract double CostoBoleto();
}
public class BoletoTurista : Boleto
{
    public override double CostoBoleto() => 99500 + 84000;
}
public class BoletoEjecutivo : Boleto
{
    public override double CostoBoleto() => 99500 + 98000;
}

// Creator: declara el Factory Method y lo usa
public abstract class Vendedor
{
    public abstract Boleto CrearBoleto();   // Factory Method
    public double Vender() => CrearBoleto().CostoBoleto();
}
public class VendedorTurista : Vendedor
{
    public override Boleto CrearBoleto() => new BoletoTurista();
}
public class VendedorEjecutivo : Vendedor
{
    public override Boleto CrearBoleto() => new BoletoEjecutivo();
}

class Program
{
    static void Main()
    {
        Vendedor v = new VendedorEjecutivo(); // la UI elige el creador, no el producto
        System.Console.WriteLine(v.Vender()); // 197500
    }
}
