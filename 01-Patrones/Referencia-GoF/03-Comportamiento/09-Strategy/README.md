# Strategy

**Categoria:** Comportamiento

**Escenario:** La forma de elegir la calidad es un algoritmo intercambiable: automatica o modo ahorro.

## Diagrama UML

```mermaid
classDiagram
    class Reproductor {
        +SetEstrategia(IEstrategiaCalidad)
        +Reproducir(int) string
    }
    class IEstrategiaCalidad {
        +Elegir(int) string
    }
    class CalidadAutomatica
    class ModoAhorro
    Reproductor o--> IEstrategiaCalidad
    IEstrategiaCalidad <|.. CalidadAutomatica
    IEstrategiaCalidad <|.. ModoAhorro
```

## Codigo

Ver [`Strategy.cs`](Strategy.cs).

## Como ejecutar

```bash
dotnet run
```
