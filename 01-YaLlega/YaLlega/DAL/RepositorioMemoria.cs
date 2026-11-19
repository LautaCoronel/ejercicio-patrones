using System.Collections.Generic;

namespace DAL;

// Repositorio en memoria (se usa en los tests).
public class RepositorioMemoria<T> : IRepositorio<T>
{
    private readonly List<T> _datos = new List<T>();
    public void Agregar(T entidad) => _datos.Add(entidad);
    public List<T> ObtenerTodos() => _datos;
}
