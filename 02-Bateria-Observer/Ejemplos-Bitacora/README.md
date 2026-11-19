# Ejemplos de bitacora (2.do parcial: SuscriptorBitacora)

Cada archivo se copia al proyecto `SistemaBateria` en la capa que dice su namespace.

| Archivo | Capa | Para que |
|---|---|---|
| `BitacoraRepositoryRotacionDiaria.cs` | DAL/Implementations | Escribe en **bitacora.txt** (como pide el enunciado) y al cambiar el dia lo renombra a `bitacora_yyyy-MM-dd.txt`. Acepta encabezado opcional para CSV. |
| `BitacoraRepositoryRotacionTamano.cs` | DAL/Implementations | Rota cuando bitacora.txt supera un tamano maximo. |
| `FormatoBitacora.cs` | BLL/Suscriptores | Formatos de linea: `Texto`, `Csv` (+ `EncabezadoCsv`), `Json`. |

## Como usarlos

En `UI/Program.cs` se cambia solo el repositorio que se crea:

```csharp
IBitacoraRepository repositorio = new BitacoraRepositoryRotacionDiaria();
// CSV con encabezado:
IBitacoraRepository repositorio = new BitacoraRepositoryRotacionDiaria(null, FormatoBitacora.EncabezadoCsv);
// Rotacion por tamano (1 KB):
IBitacoraRepository repositorio = new BitacoraRepositoryRotacionTamano(null, 1024);
```

En `BLL/Suscriptores/SuscriptorBitacora.cs`, dentro de `Actualizar`, la linea se arma con:

```csharp
string linea = FormatoBitacora.Texto(bateria);   // o Csv(bateria) / Json(bateria)
_repositorio.Escribir(linea);
```

En Visual Studio (.NET Framework) despues de copiar un archivo: clic derecho en la carpeta > Agregar > Elemento existente.
