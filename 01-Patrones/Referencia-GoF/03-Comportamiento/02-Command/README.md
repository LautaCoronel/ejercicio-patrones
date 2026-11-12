# Command

**Categoria:** Comportamiento

**Escenario:** Los botones del control (play/pause) se encapsulan como comandos, con historial para deshacer.

## Diagrama UML

```mermaid
classDiagram
    class IComando {
        +Ejecutar()
        +Deshacer()
    }
    class ComandoPlay
    class Control {
        +Presionar(IComando)
        +Deshacer()
    }
    class Reproductor {
        +Play()
        +Pause()
    }
    IComando <|.. ComandoPlay
    Control o--> IComando
    ComandoPlay --> Reproductor
```

## Codigo

Ver [`Command.cs`](Command.cs).

## Como ejecutar

```bash
dotnet run
```
