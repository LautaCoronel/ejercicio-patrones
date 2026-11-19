# YaLlega S.A. - Nucleo de plataforma de pedidos y entregas

| Requisito del enunciado | Patron / clase |
|---|---|
| Envio segun demanda (normal, +25%, +60%) y modo promocion gratis | **Strategy**: `IEstrategiaEnvio` + `TarifaNormal`, `RecargoDemandaMedia`, `RecargoDemandaAlta`, `EnvioPromocion` |
| Cambiar el calculo con el pedido abierto | `PedidoService.MedirDemanda` (cada cambio queda en `Pedido.Tarifas`) |
| Operador fuerza un modo con un boton | `PedidoService.ForzarModo` / `LiberarModo` |
| Catalogo remoto, bloquear por plan, cachear 5 menus, la pantalla no distingue | **Proxy**: `ICatalogo`, `CatalogoRemoto`, `CatalogoProxy` |
| Exportar cualquier listado con encabezados, una unica rutina | **Visitor**: `IExportVisitor`, `FilaTextoVisitor`, `EncabezadoVisitor`, `ExportService` |
| Persistir cada pedido (cliente, productos, tarifas, origen del menu, rechazados) | **Repository**: `RepositorioPedidosArchivo` -> `pedidos.txt` |
| 3 o mas capas | DomainModel, DAL, Services, BLL, UI, Tests |
| Casos de prueba por escenario | `Tests/YaLlegaTests.cs` (13 tests) |

Abrir `YaLlega.sln`, proyecto de inicio **UI**, F5. Tests: Prueba > Ejecutar todas las pruebas.
