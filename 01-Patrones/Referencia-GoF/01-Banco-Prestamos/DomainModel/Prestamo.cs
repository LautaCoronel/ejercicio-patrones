namespace DomainModel;

public class Prestamo
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public decimal Monto { get; set; }
    public bool Aprobado { get; set; }
    public string AprobadoPor { get; set; }
}
