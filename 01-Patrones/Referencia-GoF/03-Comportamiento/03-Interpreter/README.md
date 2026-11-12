# Interpreter

**Categoria:** Comportamiento

**Escenario:** Evaluar expresiones simples como 'uno mas cinco menos cuatro' y devolver un entero.

## Diagrama UML

```mermaid
classDiagram
    class IExpresion {
        +Interpretar() int
    }
    class Numero
    class Suma
    class Resta
    IExpresion <|.. Numero
    IExpresion <|.. Suma
    IExpresion <|.. Resta
    Suma o--> IExpresion
    Resta o--> IExpresion
```

## Codigo

Ver [`Interpreter.cs`](Interpreter.cs).

## Como ejecutar

```bash
dotnet run
```
