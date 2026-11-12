# Composite

**Categoria:** Estructurales

**Escenario:** Un catalogo con categorias que contienen contenidos u otras categorias (arbol).

## Diagrama UML

```mermaid
classDiagram
    class ComponenteCatalogo {
        +string Nombre
        +Mostrar(string)
    }
    class Contenido {
        +Mostrar(string)
    }
    class Categoria {
        +Agregar(ComponenteCatalogo)
        +Mostrar(string)
    }
    ComponenteCatalogo <|-- Contenido
    ComponenteCatalogo <|-- Categoria
    Categoria o--> ComponenteCatalogo : hijos
```

## Codigo

Ver [`Composite.cs`](Composite.cs).

## Como ejecutar

```bash
dotnet run
```
