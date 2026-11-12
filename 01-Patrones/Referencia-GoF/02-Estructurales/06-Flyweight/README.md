# Flyweight

**Categoria:** Estructurales

**Escenario:** Dibujar miles de particulas que comparten la misma textura (intrinseco) y difieren en posicion (extrinseco).

## Diagrama UML

```mermaid
classDiagram
    class SpriteParticula {
        -string _textura
        +Dibujar(int, int)
    }
    class SpriteFactory {
        +Obtener(string) SpriteParticula
    }
    SpriteFactory o--> SpriteParticula : pool
    Client --> SpriteFactory
```

## Codigo

Ver [`Flyweight.cs`](Flyweight.cs).

## Como ejecutar

```bash
dotnet run
```
