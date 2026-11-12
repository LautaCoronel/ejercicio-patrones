# Adapter

**Categoria:** Estructurales

**Escenario:** El sistema usa IReproductor, pero hay que integrar una libreria externa con otra API (PlayFile).

## Diagrama UML

```mermaid
classDiagram
    class IReproductor {
        +Reproducir(string)
    }
    class ReproductorAdapter {
        +Reproducir(string)
    }
    class ReproductorExterno {
        +PlayFile(string)
    }
    IReproductor <|.. ReproductorAdapter
    ReproductorAdapter o--> ReproductorExterno
```

## Codigo

Ver [`Adapter.cs`](Adapter.cs).

## Como ejecutar

```bash
dotnet run
```
