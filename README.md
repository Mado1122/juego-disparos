# Space Shooter – Raylib-cs

Juego 2D tipo space shooter hecho con **C# + Raylib-cs**. Nave, disparos, enemigos, puntuación y vidas.

## Requisitos
- .NET SDK 7 u 8
- Terminal (CMD / PowerShell / Bash)

## Cómo ejecutar

```bash
dotnet restore
dotnet run
```

## Controles
- **A / D** – Mover la nave izquierda / derecha
- **ESPACIO** – Disparar
- **R** – Reiniciar (en pantalla de Game Over)

## Mecánicas
- Destruye enemigos (cuadrados rojos) con tus disparos para sumar **10 puntos** por cada uno.
- Tienes **3 vidas**; si un enemigo te toca, pierdes una vida.
- Al quedarte sin vidas aparece **Game Over**; pulsa **R** para volver a jugar.
