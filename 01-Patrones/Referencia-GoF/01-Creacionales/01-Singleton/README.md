# Singleton

**Categoria:** Creacionales

**Escenario:** El sistema debe tener una unica sesion de usuario, accesible desde cualquier capa.

## Diagrama UML

```mermaid
classDiagram
    class SesionUsuario {
        -static _instancia SesionUsuario
        -SesionUsuario()
        +string Usuario
        +static Instancia() SesionUsuario
    }
    SesionUsuario --> SesionUsuario : crea y retorna
```

## Codigo

Ver [`Singleton.cs`](Singleton.cs).

## Como ejecutar

```bash
dotnet run
```
