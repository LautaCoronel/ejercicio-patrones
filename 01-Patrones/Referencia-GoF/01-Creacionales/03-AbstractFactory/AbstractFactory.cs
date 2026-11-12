using System;

namespace Patrones.Creacionales.AbstractFactory;

// Productos abstractos
public interface IReproductor { string Reproducir(); }
public interface IDecodificador { string Decodificar(); }

// Familia Movil
public class ReproductorMovil : IReproductor { public string Reproducir() => "video en movil"; }
public class DecodificadorMovil : IDecodificador { public string Decodificar() => "H.264"; }

// Familia TV
public class ReproductorTV : IReproductor { public string Reproducir() => "video en TV 4K"; }
public class DecodificadorTV : IDecodificador { public string Decodificar() => "H.265"; }

// Abstract Factory
public interface IDispositivoFactory
{
    IReproductor CrearReproductor();
    IDecodificador CrearDecodificador();
}
public class MovilFactory : IDispositivoFactory
{
    public IReproductor CrearReproductor() => new ReproductorMovil();
    public IDecodificador CrearDecodificador() => new DecodificadorMovil();
}
public class TVFactory : IDispositivoFactory
{
    public IReproductor CrearReproductor() => new ReproductorTV();
    public IDecodificador CrearDecodificador() => new DecodificadorTV();
}

class Program
{
    static void Main()
    {
        IDispositivoFactory f = new TVFactory(); // se elige la familia una sola vez
        var r = f.CrearReproductor();
        var d = f.CrearDecodificador();
        System.Console.WriteLine(d.Decodificar() + " -> " + r.Reproducir());
    }
}
