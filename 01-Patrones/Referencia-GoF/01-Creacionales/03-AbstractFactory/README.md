# Abstract Factory

**Categoria:** Creacionales

**Escenario:** Segun el dispositivo (Movil o TV) crear una familia de objetos que van juntos: reproductor y decodificador.

## Diagrama UML

```mermaid
classDiagram
    class IDispositivoFactory {
        +CrearReproductor() IReproductor
        +CrearDecodificador() IDecodificador
    }
    class MovilFactory
    class TVFactory
    class IReproductor
    class IDecodificador
    IDispositivoFactory <|.. MovilFactory
    IDispositivoFactory <|.. TVFactory
    MovilFactory ..> IReproductor : crea
    MovilFactory ..> IDecodificador : crea
    TVFactory ..> IReproductor : crea
    TVFactory ..> IDecodificador : crea
```

## Codigo

Ver [`AbstractFactory.cs`](AbstractFactory.cs).

## Como ejecutar

```bash
dotnet run
```
