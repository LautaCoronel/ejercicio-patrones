# Facade

**Categoria:** Estructurales

**Escenario:** Reproducir un contenido implica red, codec y buffer. La UI llama a una fachada simple.

## Diagrama UML

```mermaid
classDiagram
    class ReproductorFacade {
        +Reproducir(string)
    }
    class Red
    class Codec
    class Buffer
    ReproductorFacade --> Red
    ReproductorFacade --> Codec
    ReproductorFacade --> Buffer
```

## Codigo

Ver [`Facade.cs`](Facade.cs).

## Como ejecutar

```bash
dotnet run
```
