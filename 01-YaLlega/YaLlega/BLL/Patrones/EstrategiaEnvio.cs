namespace BLL.Patrones;

// Strategy: como se calcula el envio segun la demanda de la zona.
public interface IEstrategiaEnvio { decimal Calcular(decimal tarifa, int demanda); }

public class EnvioPorDemanda : IEstrategiaEnvio
{
    public decimal Calcular(decimal tarifa, int demanda)
        => demanda > 70 ? tarifa * 1.60m : demanda > 40 ? tarifa * 1.25m : tarifa;
}
public class ModoPromocion : IEstrategiaEnvio
{
    public decimal Calcular(decimal tarifa, int demanda) => 0m; // siempre gratis
}
