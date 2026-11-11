# Tetris

Tetris básico en 2D hecho en Unity. Las piezas caen en un tablero de 10 x 20 casillas;
acomódalas para completar líneas horizontales, que desaparecen y te dan puntos.

Todos los gráficos se generan por código: el proyecto no usa imágenes, sonidos ni otros archivos externos.

## Cómo se juega

- Aparecen al azar las 7 piezas clásicas: **I, O, T, S, Z, J y L**, cada una de un color.
- La pieza cae sola. Muévela y gírala para acomodarla.
- Cuando completas una fila, se borra y lo de arriba baja.
- **Puntaje** por líneas borradas a la vez (multiplicado por el nivel):
  1 línea = 100, 2 = 300, 3 = 500, 4 = 800. La caída suave da 1 punto por fila.
- Cada **10 líneas** subes de **nivel** y las piezas caen más rápido.
- Si una pieza nueva ya no cabe al aparecer, es **fin del juego**.

### Controles

| Tecla | Acción |
|---|---|
| `←` | Mover a la izquierda |
| `→` | Mover a la derecha |
| `↑` | Rotar |
| `↓` (mantener) | Caída suave |
| `R` | Reiniciar la partida |

## Versión de Unity

**Unity 6000.6.0f1** (tomada de `ProjectSettings/ProjectVersion.txt`).

El proyecto se creó con la plantilla **Universal 2D** y usa el **nuevo Input System**
(`com.unity.inputsystem`), que es el que la plantilla trae activado por defecto.

## Cómo abrir el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/dsanchezz/tetris.git
   ```
2. Abre **Unity Hub** → pestaña **Projects** → **Add** → **Add project from disk**.
3. Selecciona la carpeta `tetris` (la que contiene `Assets`, `Packages` y `ProjectSettings`).
4. Si Unity Hub pide instalar la versión 6000.6.0f1, instálala (o elige una versión 6000.x compatible).
5. La primera vez Unity tarda unos minutos en importar el proyecto.
6. Abre la escena `Assets/Scenes/SampleScene.unity` y presiona **Play**.

No hace falta configurar nada en la escena: el juego se crea solo al dar Play.

## Cómo está organizado el código

Todo el código está en `Assets/Scripts/`. Cada archivo tiene una sola responsabilidad:

| Archivo | Qué hace |
|---|---|
| `Arranque.cs` | Punto de entrada. Usa `[RuntimeInitializeOnLoadMethod]` para crear todo al dar Play: cámara ortográfica, tablero, control e interfaz. También reinicia el juego. |
| `FormasPiezas.cs` | Datos de las 7 piezas: la forma (4 casillas alrededor de un centro) y el color de cada una. |
| `Pieza.cs` | La pieza que cae: su tipo, posición y giro. Mover o girar devuelve una pieza nueva para poder probarla antes de aceptarla. |
| `Tablero.cs` | La cuadrícula de 10 x 20: revisa si una pieza cabe, fija piezas, borra líneas completas y dibuja las casillas. |
| `ControlJuego.cs` | Lee el teclado, hace caer la pieza (más rápido con cada nivel), suma puntos y detecta el fin del juego. |
| `Interfaz.cs` | Muestra puntaje, líneas, nivel y el mensaje de fin del juego con `OnGUI`. |
| `GeneradorSprites.cs` | Crea por código el sprite de cuadro con `Texture2D` y `Sprite.Create`. |
