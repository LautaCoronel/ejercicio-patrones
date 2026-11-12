using System;

namespace Patrones.Creacionales.Singleton;

public sealed class SesionUsuario
{
    private static SesionUsuario _instancia;
    private static readonly object _lock = new object();

    public string Usuario { get; set; }

    private SesionUsuario() { }

    public static SesionUsuario Instancia
    {
        get
        {
            lock (_lock)
            {
                if (_instancia == null)
                    _instancia = new SesionUsuario();
                return _instancia;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        SesionUsuario.Instancia.Usuario = "franco";
        System.Console.WriteLine(SesionUsuario.Instancia.Usuario); // franco
        System.Console.WriteLine(ReferenceEquals(SesionUsuario.Instancia, SesionUsuario.Instancia)); // True
    }
}
