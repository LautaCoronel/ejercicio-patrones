# Prototype

**Categoria:** Creacionales

**Escenario:** Duplicar figuras graficas clonando una existente sin depender de su clase concreta.

## Diagrama UML

```mermaid
classDiagram
    class Figura {
        +int X
        +int Y
        +Clonar() Figura
    }
    class Circulo {
        +int Radio
        +Clonar() Figura
    }
    class Rectangulo {
        +int Ancho
        +Clonar() Figura
    }
    Figura <|-- Circulo
    Figura <|-- Rectangulo
```

## Codigo

Ver [`Prototype.cs`](Prototype.cs).

## Como ejecutar

```bash
dotnet run
```
