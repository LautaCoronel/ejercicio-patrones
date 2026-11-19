using System.Linq;
using DomainModel;

namespace BLL.Patrones;

// ConcreteVisitor: arma la fila de datos de cada clase.
public class FilaTextoVisitor : IExportVisitor
{
    public string Visit(Producto p) => $"{p.Id};{p.Nombre};{p.Precio};{p.PlanMinimo}";
    public string Visit(Cliente c) => $"{c.Id};{c.Nombre};{c.Plan}";
    public string Visit(Pedido p) =>
        $"{p.Id};{p.Cliente.Nombre};{string.Join(",", p.Items.Select(i => i.Producto.Nombre))};{p.Envio};{p.Total}";
}

// ConcreteVisitor: arma el encabezado de cada clase.
public class EncabezadoVisitor : IExportVisitor
{
    public string Visit(Producto p) => "ID;NOMBRE;PRECIO;PLAN_MINIMO";
    public string Visit(Cliente c) => "ID;NOMBRE;PLAN";
    public string Visit(Pedido p) => "ID;CLIENTE;PRODUCTOS;ENVIO;TOTAL";
}
