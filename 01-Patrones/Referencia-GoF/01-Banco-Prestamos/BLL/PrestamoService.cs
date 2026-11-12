using System.Collections.Generic;
using DomainModel;
using DAL;
using Services;
using BLL.Patrones;

namespace BLL;

public class PrestamoService
{
    private readonly IRepositorio<Prestamo> _repo;
    private readonly ILogger _logger;
    private readonly Aprobador _cadena;

    public PrestamoService(IRepositorio<Prestamo> repo, ILogger logger)
    {
        _repo = repo; _logger = logger;
        var ejecutivo = new Ejecutivo();
        ejecutivo.SetSiguiente(new Lider()).SetSiguiente(new Gerente()).SetSiguiente(new Director());
        _cadena = ejecutivo;
    }

    public Prestamo Solicitar(Cliente cliente, decimal monto)
    {
        var p = new Prestamo { Cliente = cliente, Monto = monto };
        _cadena.Aprobar(p);
        _repo.Agregar(p);
        _logger.Log("Prestamo de " + monto + " aprobado por " + p.AprobadoPor);
        return p;
    }

    public List<Prestamo> Historial() => _repo.ObtenerTodos();
}
