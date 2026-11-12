using System;

namespace Patrones.Estructurales.Facade;

// Subsistemas
public class Red { public void Conectar() => System.Console.WriteLine("Conectado"); }
public class Codec { public void Decodificar() => System.Console.WriteLine("Decodificando"); }
public class Buffer { public void Precargar() => System.Console.WriteLine("Buffer listo"); }

// Facade
public class ReproductorFacade
{
    private readonly Red _red = new Red();
    private readonly Codec _codec = new Codec();
    private readonly Buffer _buffer = new Buffer();

    public void Reproducir(string titulo)
    {
        _red.Conectar();
        _buffer.Precargar();
        _codec.Decodificar();
        System.Console.WriteLine("Reproduciendo " + titulo);
    }
}

class Program
{
    static void Main()
    {
        new ReproductorFacade().Reproducir("pelicula.mp4"); // un solo metodo de alto nivel
    }
}
