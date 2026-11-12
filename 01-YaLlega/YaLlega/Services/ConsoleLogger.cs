namespace Services;

public class ConsoleLogger : ILogger
{
    public void Log(string mensaje) => System.Console.WriteLine("[LOG] " + mensaje);
}
