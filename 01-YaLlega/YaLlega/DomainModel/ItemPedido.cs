namespace DomainModel;

// Un producto dentro del pedido y de donde salio su menu.
public class ItemPedido
{
    public Producto Producto { get; set; }
    public string Origen { get; set; }      // "SERVIDOR" o "DISPOSITIVO"
}
