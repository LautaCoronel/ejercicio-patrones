# Decorator

**Categoria:** Estructurales

**Escenario:** Calcular el costo de una bebida base a la que se le agregan condimentos de forma dinamica.

## Diagrama UML

```mermaid
classDiagram
    class Bebida {
        +Costo() double
        +Descripcion() string
    }
    class Cafe
    class CondimentoDecorator {
        #Bebida _bebida
    }
    class Leche
    class Azucar
    Bebida <|-- Cafe
    Bebida <|-- CondimentoDecorator
    CondimentoDecorator o--> Bebida
    CondimentoDecorator <|-- Leche
    CondimentoDecorator <|-- Azucar
```

## Codigo

Ver [`Decorator.cs`](Decorator.cs).

## Como ejecutar

```bash
dotnet run
```
