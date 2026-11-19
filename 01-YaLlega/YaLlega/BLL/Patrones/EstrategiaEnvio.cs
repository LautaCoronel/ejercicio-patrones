namespace BLL.Patrones;

// STRATEGY: cada forma de calcular el envio es una clase.
// El PedidoService la cambia en tiempo de ejecucion (por demanda o por el operador).
public interface IEstrategiaEnvio
{
    string Nombre { get; }
    decimal Calcular(decimal tarifaBase);
}

public class TarifaNormal : IEstrategiaEnvio
{
    public string Nombre => "Normal";
    public decimal Calcular(decimal tarifaBase) => tarifaBase;
}

public class RecargoDemandaMedia : IEstrategiaEnvio
{
    public string Nombre => "Demanda media (+25%)";
    public decimal Calcular(decimal tarifaBase) => tarifaBase * 1.25m;
}

public class RecargoDemandaAlta : IEstrategiaEnvio
{
    public string Nombre => "Demanda alta (+60%)";
    public decimal Calcular(decimal tarifaBase) => tarifaBase * 1.60m;
}

public class EnvioPromocion : IEstrategiaEnvio
{
    public string Nombre => "Promocion (gratis)";
    public decimal Calcular(decimal tarifaBase) => 0m;
}
