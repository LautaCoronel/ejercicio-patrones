# Machete - Unidad 6 (Subprocesamiento)

Todo en 2 o 3 lineas, para leer la noche antes.

## Hilo

Un hilo es una ruta de ejecucion independiente dentro del mismo programa. Todo
programa arranca con uno solo, el **hilo principal**, que crean el CLR y el
sistema operativo. Recien cuando creo hilos con `new Thread(...)` el programa
pasa a ser multihilo.

**Hilo vs. proceso:** el proceso es el programa entero (`Unidad6.exe`). Los hilos
viven adentro del proceso y comparten su memoria (heap, codigo, archivos
abiertos). Lo unico que **no** comparten es la **pila**: cada hilo tiene la suya,
por eso las variables locales de un hilo no se mezclan con las del otro.

## Quien decide que hilo corre

El **planificador de hilos**. Le da a cada hilo activo un tiempo de CPU, y a los
que estan esperando o bloqueados no les da nada, asi que no consumen CPU.

- **1 nucleo:** no hay dos cosas de verdad al mismo tiempo. La CPU va cambiando
  de hilo tan rapido que parece simultaneo. Eso es **timeslicing**, y el efecto
  se llama **concurrencia**.
- **Varios nucleos:** cada hilo corre en su nucleo al mismo tiempo. Eso es
  **paralelismo** de verdad, y por eso termina antes.

## La clase Thread

Propiedades: `IsAlive` (true mientras corre), `Name` (para reconocerlo al
depurar), `Priority` (es una sugerencia al sistema, no garantiza nada).

Metodos: `Start()` lo arranca, `Sleep(ms)` pausa **solo al hilo actual**,
`Join()` espera a que el otro termine.

Un hilo termina cuando termina el metodo que le pasamos. **No se puede
reiniciar**: si lo quiero otra vez, tengo que crear otro.

## Ventajas y desventajas

**Ventajas:** aprovecha todos los nucleos, la aplicacion responde mas rapido, y
en una interfaz grafica el hilo de la UI no se congela mientras otro hace el
trabajo pesado.

**Desventajas:** es mas complicado de disenar, aparecen errores que no se repiten
siempre (condiciones de carrera y deadlocks), es mas dificil de depurar porque el
orden cambia en cada corrida, y crear hilos cuesta: **mas hilos no siempre es mas
rapido**.

## Donde se usa

Interfaz de usuario (el trabajo pesado va a un hilo aparte), operaciones que
esperan (red, disco, base de datos), calculo intensivo (divide y venceras) y
servidores que atienden varios clientes a la vez.

Regla practica: si el programa **espera** -> `async`/`await`. Si el programa
**calcula** -> `Parallel` o `Task.Run`.

## Programacion paralela

`Parallel.For` y `Parallel.ForEach` hacen lo mismo que un `for`/`foreach` pero
reparten las vueltas entre los nucleos, y no siguen hasta que terminan todas. La
ganancia no es la ideal porque repartir el trabajo y juntar los resultados tambien
cuesta tiempo.

## Sincronizacion

Sincronizar es coordinar los hilos para que el resultado sea siempre el mismo.

**Condicion de carrera:** `contador++` no es un solo paso, son tres (leer, sumar,
escribir). Si dos hilos leen el mismo 5 antes de que el otro escriba, se pierde
una suma. El resultado cambia en cada corrida, y ese bug puede aparecer recien en
produccion.

Hay tres tipos de bloqueo:

| Tipo | Para que | Cuales son |
|---|---|---|
| Simple | Esperar a que otro termine o pase un tiempo | `Sleep`, `Join`, `Task.Wait` |
| Exclusivo | Varios hilos compiten por el mismo dato: entran de a uno | `lock`, `Monitor`, `Mutex` |
| No exclusivo | Limitar cuantos entran a la vez | `SemaphoreSlim(n)` |

`lock (candado) { ... }`: solo un hilo por vez adentro. El candado va en un objeto
**privado**, y se bloquea solo la parte necesaria, no todo el metodo.

`SemaphoreSlim(3)`: como un ascensor para 3. `Wait()` resta un lugar (si no hay,
espera) y `Release()` lo devuelve.

## Task y async/await

`Task` es una abstraccion de `Thread`: "un lugar donde ejecutar algo" que promete
un resultado para despues. `Task.Run(...)` lo corre en el ThreadPool (hilos
reutilizados), asi no creo hilos a mano.

`await` espera el resultado **sin bloquear el hilo**. `Task.WhenAll` espera a
varias tareas y sigue cuando terminaron todas: si son de 2, 3 y 2,5 segundos,
tarda ~3 y no 7,5.

Es la forma recomendada hoy para todo lo que espera (red, disco, base de datos).

## Las respuestas cortas

- **Que es un hilo?** Una ruta de ejecucion independiente que corre junto a otras
  dentro del mismo proceso.
- **Que comparten los hilos?** La memoria del proceso. Lo que no comparten es la
  pila.
- **Quien los maneja?** El planificador de hilos del sistema operativo.
- **Timeslicing o paralelismo?** Timeslicing es un solo nucleo alternando rapido
  (concurrencia). Paralelismo es varios nucleos a la vez, de verdad.
- **Que resuelve el lock?** Que dos hilos no toquen el mismo dato al mismo
  tiempo.
- **Que es una condicion de carrera?** Dos hilos leen y escriben el mismo dato y
  el resultado depende de quien llego primero.
- **Thread o Task?** Task, porque reutiliza hilos del ThreadPool y es mas barato.
- **Que hace Join?** Bloquea al hilo que lo llama hasta que el otro termina.
- **Diferencia entre await y Thread.Sleep?** `await` libera el hilo mientras
  espera; `Sleep` lo deja bloqueado.
- **Por que un hilo no se puede reiniciar?** Porque termina cuando termina su
  metodo; para volver a correrlo hay que crear un hilo nuevo.
