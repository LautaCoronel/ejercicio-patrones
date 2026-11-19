using System.Collections.Generic;
using System.Linq;

namespace DomainModel;

public class Pedido : IExportable
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public bool Abierto { get; set; } = true;
    public List<ItemPedido> Items { get; } = new List<ItemPedido>();
    public List<TarifaAplicada> Tarifas { get; } = new List<TarifaAplicada>();
    public List<string> Rechazados { get; } = new List<string>();   // productos rechazados por plan

    public decimal Subtotal => Items.Sum(i => i.Producto.Precio);
    public decimal Envio => Tarifas.Count == 0 ? 0 : Tarifas.Last().Monto;
    public decimal Total => Subtotal + Envio;

    public string Accept(IExportVisitor v) => v.Visit(this);
}
