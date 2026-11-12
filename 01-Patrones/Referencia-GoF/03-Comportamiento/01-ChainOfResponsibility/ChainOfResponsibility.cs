using System;

namespace Patrones.Comportamiento.ChainOfResponsibility;

public abstract class Validador
{
    private Validador _siguiente;
    public Validador SetSiguiente(Validador s) { _siguiente = s; return s; }
    public virtual bool Validar(string usuario)
        => _siguiente == null ? true : _siguiente.Validar(usuario);
}
public class ValidaPlan : Validador
{
    public override bool Validar(string u)
    {
        System.Console.WriteLine("Valida plan");
        return base.Validar(u);
    }
}
public class ValidaDisponibilidad : Validador
{
    public override bool Validar(string u)
    {
        System.Console.WriteLine("Valida disponibilidad");
        return base.Validar(u);
    }
}

class Program
{
    static void Main()
    {
        var plan = new ValidaPlan();
        plan.SetSiguiente(new ValidaDisponibilidad());
        System.Console.WriteLine(plan.Validar("franco")); // True
    }
}
