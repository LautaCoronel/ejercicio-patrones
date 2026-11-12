using System;

namespace Patrones.Comportamiento.State;

// State
public interface IEstadoConexion { string Calidad(); }
public class BuenaConexion : IEstadoConexion { public string Calidad() => "4K"; }
public class ConexionMedia : IEstadoConexion { public string Calidad() => "HD"; }
public class MalaConexion : IEstadoConexion { public string Calidad() => "baja"; }

// Context
public class Reproductor
{
    private IEstadoConexion _estado = new BuenaConexion();
    public void SetEstado(IEstadoConexion e) => _estado = e;
    public string Reproducir() => "Reproduciendo en " + _estado.Calidad();
}

class Program
{
    static void Main()
    {
        var r = new Reproductor();
        System.Console.WriteLine(r.Reproducir()); // 4K
        r.SetEstado(new MalaConexion());          // la conexion empeora en runtime
        System.Console.WriteLine(r.Reproducir()); // baja
    }
}
