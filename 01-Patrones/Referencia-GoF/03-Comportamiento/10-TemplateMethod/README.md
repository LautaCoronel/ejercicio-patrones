# Template Method

**Categoria:** Comportamiento

**Escenario:** Exportar un listado tiene un esqueleto fijo (encabezado + cuerpo), pero cada formato completa los pasos.

## Diagrama UML

```mermaid
classDiagram
    class Exportador {
        +Exportar() string
        #Encabezado() string
        #Cuerpo() string
    }
    class ExportadorCsv
    class ExportadorTexto
    Exportador <|-- ExportadorCsv
    Exportador <|-- ExportadorTexto
```

## Codigo

Ver [`TemplateMethod.cs`](TemplateMethod.cs).

## Como ejecutar

```bash
dotnet run
```
