# Observer

**Categoria:** Comportamiento

**Escenario:** Cuando un canal publica un contenido nuevo, todos los suscriptores se enteran automaticamente.

## Diagrama UML

```mermaid
classDiagram
    class Canal {
        +Suscribir(IObservador)
        +Desuscribir(IObservador)
        +Publicar(string)
    }
    class IObservador {
        +Actualizar(string)
    }
    class UsuarioSuscriptor
    IObservador <|.. UsuarioSuscriptor
    Canal o--> IObservador : observadores
```

## Codigo

Ver [`Observer.cs`](Observer.cs).

## Como ejecutar

```bash
dotnet run
```
