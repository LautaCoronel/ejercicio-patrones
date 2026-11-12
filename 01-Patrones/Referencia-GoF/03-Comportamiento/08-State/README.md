# State

**Categoria:** Comportamiento

**Escenario:** La reproduccion cambia su calidad segun el estado de la conexion, que puede cambiar en runtime.

## Diagrama UML

```mermaid
classDiagram
    class Reproductor {
        +SetEstado(IEstadoConexion)
        +Reproducir() string
    }
    class IEstadoConexion {
        +Calidad() string
    }
    class BuenaConexion
    class ConexionMedia
    class MalaConexion
    Reproductor o--> IEstadoConexion
    IEstadoConexion <|.. BuenaConexion
    IEstadoConexion <|.. ConexionMedia
    IEstadoConexion <|.. MalaConexion
```

## Codigo

Ver [`State.cs`](State.cs).

## Como ejecutar

```bash
dotnet run
```
