# Patrones de Diseno (GoF 23) en C#

Ejemplos directos de los **23 patrones de diseno del GoF**, uno por carpeta, listos para estudiar y adaptar (p. ej. para el parcial de Trabajo de Diploma / Practicas Profesionalizantes III).

Cada patron es un **proyecto de consola independiente**: entra a su carpeta y corre `dotnet run`. Incluye un `README.md` con su escenario y diagrama UML (que GitHub renderiza), el `.cs` con el codigo y un `.csproj`.

## Requisitos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)

## Como correr un ejemplo

```bash
cd 01-Creacionales/01-Singleton
dotnet run
```

## Indice

| Patron | Categoria | Carpeta |
| --- | --- | --- |
| Singleton | Creacionales | [`01-Creacionales/01-Singleton`](01-Creacionales/01-Singleton) |
| Factory Method | Creacionales | [`01-Creacionales/02-FactoryMethod`](01-Creacionales/02-FactoryMethod) |
| Abstract Factory | Creacionales | [`01-Creacionales/03-AbstractFactory`](01-Creacionales/03-AbstractFactory) |
| Builder | Creacionales | [`01-Creacionales/04-Builder`](01-Creacionales/04-Builder) |
| Prototype | Creacionales | [`01-Creacionales/05-Prototype`](01-Creacionales/05-Prototype) |
| Adapter | Estructurales | [`02-Estructurales/01-Adapter`](02-Estructurales/01-Adapter) |
| Bridge | Estructurales | [`02-Estructurales/02-Bridge`](02-Estructurales/02-Bridge) |
| Composite | Estructurales | [`02-Estructurales/03-Composite`](02-Estructurales/03-Composite) |
| Decorator | Estructurales | [`02-Estructurales/04-Decorator`](02-Estructurales/04-Decorator) |
| Facade | Estructurales | [`02-Estructurales/05-Facade`](02-Estructurales/05-Facade) |
| Flyweight | Estructurales | [`02-Estructurales/06-Flyweight`](02-Estructurales/06-Flyweight) |
| Proxy | Estructurales | [`02-Estructurales/07-Proxy`](02-Estructurales/07-Proxy) |
| Chain of Responsibility | Comportamiento | [`03-Comportamiento/01-ChainOfResponsibility`](03-Comportamiento/01-ChainOfResponsibility) |
| Command | Comportamiento | [`03-Comportamiento/02-Command`](03-Comportamiento/02-Command) |
| Interpreter | Comportamiento | [`03-Comportamiento/03-Interpreter`](03-Comportamiento/03-Interpreter) |
| Iterator | Comportamiento | [`03-Comportamiento/04-Iterator`](03-Comportamiento/04-Iterator) |
| Mediator | Comportamiento | [`03-Comportamiento/05-Mediator`](03-Comportamiento/05-Mediator) |
| Memento | Comportamiento | [`03-Comportamiento/06-Memento`](03-Comportamiento/06-Memento) |
| Observer | Comportamiento | [`03-Comportamiento/07-Observer`](03-Comportamiento/07-Observer) |
| State | Comportamiento | [`03-Comportamiento/08-State`](03-Comportamiento/08-State) |
| Strategy | Comportamiento | [`03-Comportamiento/09-Strategy`](03-Comportamiento/09-Strategy) |
| Template Method | Comportamiento | [`03-Comportamiento/10-TemplateMethod`](03-Comportamiento/10-TemplateMethod) |
| Visitor | Comportamiento | [`03-Comportamiento/11-Visitor`](03-Comportamiento/11-Visitor) |

## Categorias

- **Creacionales** (5): abstraen la creacion de objetos.
- **Estructurales** (7): combinan clases y objetos en estructuras mas grandes.
- **De comportamiento** (11): reparten responsabilidades y comunicacion entre objetos.

## Nota

Varios ejemplos (Strategy, State, Proxy, Visitor, Factory) estan pensados sobre un caso tipo plataforma de video (SarazaFlix), que es el estilo de consigna del parcial: un escenario que combina varios patrones.
