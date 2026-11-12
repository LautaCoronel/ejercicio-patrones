# YaLlega - Nucleo de plataforma de pedidos y entregas

**Patrones:** Strategy (costo de envio), Proxy (menu remoto + plan + cache), Visitor (exportar cualquier listado), Repository (persistir pedidos).

| Capa | Contenido |
| --- | --- |
| DomainModel | Plan, Producto, Cliente, Pedido, IExportable, IExportVisitor |
| DAL | IRepositorio, RepositorioMemoria |
| Services | ILogger, ConsoleLogger |
| BLL | Patrones/EstrategiaEnvio, Patrones/MenuProxy, Patrones/TextoVisitor, PedidoService, ExportService, AppFactory |
| UI | Program (arma pedido, bloqueo por plan, modo promocion, exporta) |
| Tests | YaLlegaTests |

Mapa enunciado -> patron:
- Envio segun demanda / modo promocion -> **Strategy** (o State si lo ves como estados).
- Menu remoto + bloquear por plan + cachear 5 -> **Proxy**.
- Exportar cualquier listado con una unica rutina -> **Visitor**.
- Persistir cada pedido -> **Repository** (DAL).
- 3+ capas -> esta misma estructura.

Abri `YaLlega.sln`, inicio UI, F5. Tests con `dotnet test`.
