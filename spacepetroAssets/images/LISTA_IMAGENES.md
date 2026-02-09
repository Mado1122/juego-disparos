# Sprites / imágenes para el juego

Coloca estos archivos **en esta misma carpeta**:  
`juego disparos rayib/assets/images/`

Formato: **PNG** con fondo transparente (alpha) para que se vea bien sobre el juego.

---

| Archivo | Qué representa | Tamaño recomendado | Notas |
|---------|----------------|---------------------|--------|
| **player.png** | Tu nave / cara de Petro | 64×64 o 80×80 px | El pueblo creciendo y luchando. Se dibuja centrado; si no existe, se usa un triángulo blanco. |
| **enemy.png** | Enemigo capitalista | 48×48 px | Enemigos que bajan. Si no existe, se usa un cuadrado rojo. |
| **boss.png** | Jefe final (Trump) | 128×128 o 96×96 px | Aparece en niveles de jefe (ej. 5, 10, 15…). Si no existe, se usa un rectángulo grande rojo. |
| **weapon_drop.png** | Arma que sueltan enemigos | 32×32 px | Power-up que cae al destruir enemigos. Si no existe, se usa un pequeño rectángulo amarillo. |
| **wall.png** | Muro / defensa abajo | 60×120 px (o ancho libre × 60–120) | Se dibuja abajo con altura 60 px; se recorta por la izquierda según la vida. Si no existe, se dibuja una barra de colores. |

---

- **player.png**: La “nave” es la cara de Petro; el juego la escala al tamaño de la nave. Puedes usar una foto recortada con fondo transparente.
- **enemy.png**: Puedes usar símbolos del capitalismo (billetes, corporaciones, etc.) o enemigos genéricos.
- **boss.png**: Trump como jefe final; tamaño más grande para que destaque.
- **weapon_drop.png**: Icono de arma/power-up (bala, estrella, etc.).
- **wall.png**: Imagen del muro (ladrillos, barrera, etc.). El juego la muestra en la parte inferior y la “acorta” según el daño; mejor si es una franja horizontal.

Si falta algún archivo, el juego sigue funcionando y usa formas geométricas de colores en su lugar.
