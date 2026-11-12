using DomainModel;

namespace BLL.Patrones;

// Chain of Responsibility: cada nivel aprueba o delega al siguiente.
public abstract class Aprobador
{
    private Aprobador _siguiente;
    public Aprobador SetSiguiente(Aprobador s) { _siguiente = s; return s; }
    public abstract void Aprobar(Prestamo p);
    protected void Pasar(Prestamo p) { if (_siguiente != null) _siguiente.Aprobar(p); }
}

public class Ejecutivo : Aprobador
{
    public override void Aprobar(Prestamo p)
    {
        if (p.Monto <= 100000m) { p.Aprobado = true; p.AprobadoPor = "Ejecutivo"; }
        else Pasar(p);
    }
}
public class Lider : Aprobador
{
    public override void Aprobar(Prestamo p)
    {
        if (p.Monto <= 500000m) { p.Aprobado = true; p.AprobadoPor = "Lider"; }
        else Pasar(p);
    }
}
public class Gerente : Aprobador
{
    public override void Aprobar(Prestamo p)
    {
        if (p.Monto <= 1000000m) { p.Aprobado = true; p.AprobadoPor = "Gerente"; }
        else Pasar(p);
    }
}
public class Director : Aprobador
{
    public override void Aprobar(Prestamo p)
    {
        p.Aprobado = true; p.AprobadoPor = "Director";
    }
}
