# Memento

**Categoria:** Comportamiento

**Escenario:** Guardar la posicion de reproduccion y poder volver a ella, sin exponer el estado interno.

## Diagrama UML

```mermaid
classDiagram
    class Reproductor {
        +int Posicion
        +Guardar() Memento
        +Restaurar(Memento)
    }
    class Memento {
        +int Posicion
    }
    class Historial {
        +Push(Memento)
        +Pop() Memento
    }
    Reproductor ..> Memento : crea
    Historial o--> Memento : custodia
```

## Codigo

Ver [`Memento.cs`](Memento.cs).

## Como ejecutar

```bash
dotnet run
```
