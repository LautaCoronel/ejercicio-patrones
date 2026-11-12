using System;
using System.Collections.Generic;

namespace Patrones.Comportamiento.Iterator;

// Iterator
public interface IIterador
{
    bool HaySiguiente();
    string Siguiente();
}
public class PlaylistIterador : IIterador
{
    private readonly List<string> _temas;
    private int _pos = 0;
    public PlaylistIterador(List<string> temas) { _temas = temas; }
    public bool HaySiguiente() => _pos < _temas.Count;
    public string Siguiente() => _temas[_pos++];
}

// Agregado
public class Playlist
{
    private readonly List<string> _temas = new List<string>();
    public void Agregar(string t) => _temas.Add(t);
    public IIterador CrearIterador() => new PlaylistIterador(_temas);
}

class Program
{
    static void Main()
    {
        var p = new Playlist();
        p.Agregar("Tema 1"); p.Agregar("Tema 2");
        var it = p.CrearIterador();
        while (it.HaySiguiente())
            System.Console.WriteLine(it.Siguiente());
    }
}
