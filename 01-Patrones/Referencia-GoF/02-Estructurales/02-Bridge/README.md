# Bridge

**Categoria:** Estructurales

**Escenario:** Un control remoto (abstraccion) maneja distintos dispositivos (implementacion) sin multiplicar subclases.

## Diagrama UML

```mermaid
classDiagram
    class Control {
        #IDispositivo _d
        +Prender()
    }
    class ControlAvanzado {
        +Silenciar()
    }
    class IDispositivo {
        +Encender()
        +SetVolumen(int)
    }
    class TV
    class Radio
    Control o--> IDispositivo
    Control <|-- ControlAvanzado
    IDispositivo <|.. TV
    IDispositivo <|.. Radio
```

## Codigo

Ver [`Bridge.cs`](Bridge.cs).

## Como ejecutar

```bash
dotnet run
```
