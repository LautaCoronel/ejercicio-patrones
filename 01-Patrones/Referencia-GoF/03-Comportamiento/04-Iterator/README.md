# Iterator

**Categoria:** Comportamiento

**Escenario:** Recorrer una playlist sin exponer como guarda los temas internamente.

## Diagrama UML

```mermaid
classDiagram
    class IIterador {
        +HaySiguiente() bool
        +Siguiente() string
    }
    class PlaylistIterador
    class Playlist {
        +Agregar(string)
        +CrearIterador() IIterador
    }
    IIterador <|.. PlaylistIterador
    Playlist ..> PlaylistIterador : crea
```

## Codigo

Ver [`Iterator.cs`](Iterator.cs).

## Como ejecutar

```bash
dotnet run
```
