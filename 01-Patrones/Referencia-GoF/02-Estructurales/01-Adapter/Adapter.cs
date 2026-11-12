using System;

namespace Patrones.Estructurales.Adapter;

// Lo que el cliente espera
public interface IReproductor { void Reproducir(string archivo); }

// Clase existente incompatible (libreria externa)
public class ReproductorExterno
{
    public void PlayFile(string path) => System.Console.WriteLine("Reproduciendo " + path);
}

// Adapter: IReproductor -> ReproductorExterno
public class ReproductorAdapter : IReproductor
{
    private readonly ReproductorExterno _externo = new ReproductorExterno();
    public void Reproducir(string archivo) => _externo.PlayFile(archivo);
}

class Program
{
    static void Main()
    {
        IReproductor r = new ReproductorAdapter();
        r.Reproducir("pelicula.mp4");
    }
}
