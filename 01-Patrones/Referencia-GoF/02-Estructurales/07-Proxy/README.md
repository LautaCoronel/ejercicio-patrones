# Proxy

**Categoria:** Estructurales

**Escenario:** El contenido vive en un servidor remoto. El proxy bloquea segun el plan, cachea los ultimos 5 y expone la misma interfaz.

## Diagrama UML

```mermaid
classDiagram
    class IContenido {
        +Reproducir(string) string
    }
    class ContenidoRemoto {
        +Reproducir(string) string
    }
    class ContenidoProxy {
        +Reproducir(string) string
    }
    IContenido <|.. ContenidoRemoto
    IContenido <|.. ContenidoProxy
    ContenidoProxy o--> ContenidoRemoto : controla acceso
    Client --> IContenido
```

## Codigo

Ver [`Proxy.cs`](Proxy.cs).

## Como ejecutar

```bash
dotnet run
```
