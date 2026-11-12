using System;

namespace Patrones.Creacionales.Builder;

public class Reproduccion
{
    public string Calidad { get; set; }
    public bool Subtitulos { get; set; }
    public string Idioma { get; set; }
    public override string ToString() => $"{Calidad}, subs={Subtitulos}, {Idioma}";
}

public interface IReproduccionBuilder
{
    IReproduccionBuilder ConCalidad(string c);
    IReproduccionBuilder ConSubtitulos(bool s);
    IReproduccionBuilder ConIdioma(string i);
    Reproduccion Build();
}

public class ReproduccionBuilder : IReproduccionBuilder
{
    private readonly Reproduccion _r = new Reproduccion();
    public IReproduccionBuilder ConCalidad(string c) { _r.Calidad = c; return this; }
    public IReproduccionBuilder ConSubtitulos(bool s) { _r.Subtitulos = s; return this; }
    public IReproduccionBuilder ConIdioma(string i) { _r.Idioma = i; return this; }
    public Reproduccion Build() => _r;
}

// Director: arma configuraciones predefinidas
public class Director
{
    public Reproduccion Cine(IReproduccionBuilder b) =>
        b.ConCalidad("4K").ConSubtitulos(true).ConIdioma("es").Build();
}

class Program
{
    static void Main()
    {
        var r = new Director().Cine(new ReproduccionBuilder());
        System.Console.WriteLine(r); // 4K, subs=True, es
    }
}
