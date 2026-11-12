# Banco - Aprobacion de prestamos

**Patron:** Chain of Responsibility (comportamiento).
**Enunciado:** segun el monto aprueba Ejecutivo (<=100k), Lider (<=500k), Gerente (<=1M) o Director (>1M).

Abri `Banco.sln`, proyecto de inicio UI, F5. Tests con `dotnet test`.

| Capa | Contenido |
| --- | --- |
| DomainModel | Cliente, Prestamo |
| DAL | IRepositorio, RepositorioMemoria |
| Services | ILogger, ConsoleLogger |
| BLL | Patrones/Aprobacion (cadena), PrestamoService, AppFactory |
| UI | Program (prueba los 4 montos) |
| Tests | PrestamoServiceTests (un caso por nivel) |

Nota: el enunciado original dice "No implementar DAL"; aca se incluye para practicar la arquitectura completa. Si lo piden sin DAL, borra el proyecto DAL y guarda la lista en el service.
