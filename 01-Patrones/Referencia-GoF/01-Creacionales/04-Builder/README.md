# Builder

**Categoria:** Creacionales

**Escenario:** Armar una configuracion de reproduccion compleja (calidad, subtitulos, idioma) paso a paso.

## Diagrama UML

```mermaid
classDiagram
    class Director {
        +Cine(IReproduccionBuilder) Reproduccion
    }
    class IReproduccionBuilder {
        +ConCalidad(string) IReproduccionBuilder
        +ConSubtitulos(bool) IReproduccionBuilder
        +Build() Reproduccion
    }
    class ReproduccionBuilder
    class Reproduccion
    IReproduccionBuilder <|.. ReproduccionBuilder
    Director o--> IReproduccionBuilder
    ReproduccionBuilder ..> Reproduccion : arma
```

## Codigo

Ver [`Builder.cs`](Builder.cs).

## Como ejecutar

```bash
dotnet run
```
