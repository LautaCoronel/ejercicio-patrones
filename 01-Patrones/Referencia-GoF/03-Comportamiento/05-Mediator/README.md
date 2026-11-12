# Mediator

**Categoria:** Comportamiento

**Escenario:** Una sala de chat coordina los mensajes entre usuarios; los usuarios solo hablan con el mediador.

## Diagrama UML

```mermaid
classDiagram
    class IMediador {
        +Enviar(string, Usuario)
    }
    class SalaChat {
        +Registrar(Usuario)
        +Enviar(string, Usuario)
    }
    class Usuario {
        +Enviar(string)
        +Recibir(string)
    }
    IMediador <|.. SalaChat
    Usuario o--> IMediador
    SalaChat o--> Usuario
```

## Codigo

Ver [`Mediator.cs`](Mediator.cs).

## Como ejecutar

```bash
dotnet run
```
