# Parcial 4 - Subprocesamiento

Simular un supermercado con 3 cajas y 10 clientes que llegan al mismo tiempo.

1. **Clientes**: cada cliente es una `Task`, con un nombre y un monto de compra al azar.
2. **Cajas**: solo 3 clientes pueden pagar a la vez -> `SemaphoreSlim(3)`.
3. **Recaudacion**: un total compartido que se actualiza sin errores -> `lock`.
4. **Final**: esperar a todos con `await Task.WhenAll` y mostrar el total recaudado.

**Extra**: mostrar con `Stopwatch` cuanto tarda con 1, 3 y 10 cajas.
