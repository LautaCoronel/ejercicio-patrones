# Chain of Responsibility

**Categoria:** Comportamiento

**Escenario:** Antes de reproducir hay que pasar una cadena de validaciones (plan, disponibilidad...).

## Diagrama UML

```mermaid
classDiagram
    class Validador {
        -Validador _siguiente
        +SetSiguiente(Validador) Validador
        +Validar(string) bool
    }
    class ValidaPlan
    class ValidaDisponibilidad
    Validador <|-- ValidaPlan
    Validador <|-- ValidaDisponibilidad
    Validador o--> Validador : siguiente
```

## Codigo

Ver [`ChainOfResponsibility.cs`](ChainOfResponsibility.cs).

## Como ejecutar

```bash
dotnet run
```
