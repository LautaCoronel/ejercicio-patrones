# Practica - Subprocesamiento (Unidad 6)

Practica para el parcial de Subprocesamiento: un supermercado con 10 clientes y
3 cajas, mas cinco ejercicios cortos de hilos.

## Contenido

| Carpeta / archivo | Que hay |
|---|---|
| `Supermercado/` | El ejercicio principal: `Task`, semaforo y `lock` |
| `Ejercicios/` | Cinco ejercicios cortos: hilos, condicion de carrera, semaforo, `Parallel` y `async`/`await` |
| `Machete.md` | Resumen de la teoria de la unidad |
| `Enunciado.md` | Consigna del ejercicio principal |

## Como ejecutar

Abrir `Practica-PP3-Subprocesamiento.sln` en Visual Studio, o por consola:

```
dotnet run --project Supermercado/Supermercado.csproj -c Release
dotnet run --project Ejercicios/Ejercicios.csproj -c Release
```

## El supermercado

10 clientes llegan juntos y tienen que pagar en 3 cajas. Cada cliente es una
`Task`, las cajas se reparten con un `SemaphoreSlim` y la plata se suma con
`lock` para que no se pisen los cobros.

Se corre tres veces, con 1, 3 y 10 cajas, para comparar.

Salida real:

```
Con 1 caja(s):
Recaudado: $47110 en 10 cobros
Tardo 5094 ms

Con 3 caja(s):
Recaudado: $47110 en 10 cobros
Tardo 2039 ms

Con 10 caja(s):
Recaudado: $47110 en 10 cobros
Tardo 515 ms
```

Lo que demuestra:

- **La plata da lo mismo con 1, 3 o 10 cajas**: el `lock` evita que dos cobros se
  pisen. El numero cambia en cada corrida porque los montos son al azar, pero
  dentro de la misma corrida los tres totales son iguales.
- **El tiempo baja al agregar cajas** (5094 -> 2039 -> 515 ms): con 1 caja es una
  fila, con 10 pagan todos a la vez.
- **Nunca hay mas de 3 pagando juntos** con 3 cajas: eso lo controla el
  `SemaphoreSlim`.

## Los ejercicios

| Ejercicio | Que muestra | Resultado |
|---|---|---|
| 1. Dos hilos | Los hilos se reparten la CPU | Las letras salen mezcladas |
| 2. Condicion de carrera | `contador++` no es atomico | Sin `lock` dio **1080756**; con `lock` **2000000** |
| 3. Semaforo | `SemaphoreSlim(3)` como ascensor | Nunca hay mas de 3 arriba |
| 4. `for` vs `Parallel.For` | Repartir el trabajo entre nucleos | **30 ms** contra **10 ms** con 12 nucleos |
| 5. `async`/`await` | Esperar sin bloquear el hilo | **3012 ms** en vez de 7500 |
