using System.Collections.Generic;

namespace DAL;

public interface IRepositorio<T>
{
    void Agregar(T entidad);
    List<T> ObtenerTodos();
}
