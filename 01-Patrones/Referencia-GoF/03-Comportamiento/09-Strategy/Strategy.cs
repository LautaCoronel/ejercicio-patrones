using System;

namespace Patrones.Comportamiento.Strategy;

// Strategy
public interface IEstrategiaCalidad { string Elegir(int anchoBanda); }
public class CalidadAutomatica : IEstrategiaCalidad
{
    public string Elegir(int anchoBanda)
        => anchoBanda > 50 ? "4K" : anchoBanda > 20 ? "HD" : "baja";
}
public class ModoAhorro : IEstrategiaCalidad
{
    public string Elegir(int anchoBanda) => "baja"; // siempre baja
}

// Context
public class Reproductor
{
    private IEstrategiaCalidad _estrategia = new CalidadAutomatica();
    public void SetEstrategia(IEstrategiaCalidad e) => _estrategia = e;
    public string Reproducir(int anchoBanda) => "Calidad: " + _estrategia.Elegir(anchoBanda);
}

class Program
{
    static void Main()
    {
        var r = new Reproductor();
        System.Console.WriteLine(r.Reproducir(60)); // 4K
        r.SetEstrategia(new ModoAhorro());          // el usuario fuerza el modo
        System.Console.WriteLine(r.Reproducir(60)); // baja
    }
}
