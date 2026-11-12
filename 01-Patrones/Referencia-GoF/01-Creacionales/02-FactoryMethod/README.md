# Factory Method

**Categoria:** Creacionales

**Escenario:** Vender un Boleto cuyo subtipo (Turista/Ejecutivo) no debe instanciar la UI directamente.

## Diagrama UML

```mermaid
classDiagram
    class Boleto {
        +CostoBoleto() double
    }
    class BoletoTurista
    class BoletoEjecutivo
    class Vendedor {
        +CrearBoleto() Boleto
        +Vender() double
    }
    class VendedorTurista
    class VendedorEjecutivo
    Boleto <|-- BoletoTurista
    Boleto <|-- BoletoEjecutivo
    Vendedor <|-- VendedorTurista
    Vendedor <|-- VendedorEjecutivo
    VendedorTurista ..> BoletoTurista : crea
    VendedorEjecutivo ..> BoletoEjecutivo : crea
```

## Codigo

Ver [`FactoryMethod.cs`](FactoryMethod.cs).

## Como ejecutar

```bash
dotnet run
```
