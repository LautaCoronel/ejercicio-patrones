# Visitor

**Categoria:** Comportamiento

**Escenario:** Marketing quiere exportar cualquier listado (contenidos, usuarios...) a texto con una unica rutina.

## Diagrama UML

```mermaid
classDiagram
    class IExportVisitor {
        +Visit(Contenido) string
        +Visit(Usuario) string
    }
    class TextoVisitor
    class IExportable {
        +Accept(IExportVisitor) string
    }
    class Contenido
    class Usuario
    IExportVisitor <|.. TextoVisitor
    IExportable <|.. Contenido
    IExportable <|.. Usuario
    Contenido ..> IExportVisitor
    Usuario ..> IExportVisitor
```

## Codigo

Ver [`Visitor.cs`](Visitor.cs).

## Como ejecutar

```bash
dotnet run
```
