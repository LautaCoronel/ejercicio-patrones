using System;
using System.Collections.Generic;

namespace Patrones.Estructurales.Flyweight;

// Flyweight: estado intrinseco compartido (el sprite pesado)
public class SpriteParticula
{
    private readonly string _textura;
    public SpriteParticula(string textura) { _textura = textura; }
    public void Dibujar(int x, int y) // extrinseco: posicion
        => System.Console.WriteLine($"{_textura} en ({x},{y})");
}

// Factory: reutiliza instancias
public class SpriteFactory
{
    private readonly Dictionary<string, SpriteParticula> _pool = new Dictionary<string, SpriteParticula>();
    public SpriteParticula Obtener(string textura)
    {
        if (!_pool.ContainsKey(textura))
            _pool[textura] = new SpriteParticula(textura);
        return _pool[textura];
    }
    public int Creados => _pool.Count;
}

class Program
{
    static void Main()
    {
        var f = new SpriteFactory();
        for (int i = 0; i < 1000; i++)
            f.Obtener("humo.png").Dibujar(i, i); // 1000 particulas, 1 sprite
        System.Console.WriteLine("Sprites creados: " + f.Creados); // 1
    }
}
