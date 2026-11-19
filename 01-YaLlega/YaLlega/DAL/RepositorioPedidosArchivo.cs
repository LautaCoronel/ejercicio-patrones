using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DomainModel;

namespace DAL;

// Persiste cada pedido en un archivo de texto con formato personalizado:
// cliente, productos (con origen del menu), tarifas aplicadas y rechazados por plan.
public class RepositorioPedidosArchivo : IRepositorio<Pedido>
{
    private readonly string _ruta;
    private readonly List<Pedido> _pedidos = new List<Pedido>();

    public RepositorioPedidosArchivo(string ruta = "pedidos.txt")
    {
        _ruta = ruta;
    }

    public void Agregar(Pedido p)
    {
        if (p == null) throw new ArgumentNullException(nameof(p));

        var sb = new StringBuilder();
        sb.AppendLine($"=== PEDIDO #{p.Id} | {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
        sb.AppendLine($"CLIENTE: {p.Cliente.Nombre} | PLAN: {p.Cliente.Plan}");
        foreach (var i in p.Items)
            sb.AppendLine($"PRODUCTO: {i.Producto.Nombre} | ${i.Producto.Precio} | MENU DESDE: {i.Origen}");
        foreach (var t in p.Tarifas)
            sb.AppendLine($"TARIFA: {t.Modo} | ${t.Monto}");
        foreach (var r in p.Rechazados)
            sb.AppendLine($"RECHAZADO POR PLAN: {r}");
        sb.AppendLine($"TOTAL: ${p.Total}");
        sb.AppendLine();

        try
        {
            // AppendAllText crea el archivo si no existe o agrega al final.
            File.AppendAllText(_ruta, sb.ToString(), Encoding.UTF8);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("No se pudo guardar el pedido: " + ex.Message, ex);
        }
        _pedidos.Add(p);
    }

    public List<Pedido> ObtenerTodos() => _pedidos;
}
