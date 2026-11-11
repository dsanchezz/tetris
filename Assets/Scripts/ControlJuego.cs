using UnityEngine;
using UnityEngine.InputSystem; // el proyecto usa el nuevo Input System

// Controla la partida: lee el teclado, hace caer la pieza, suma puntos,
// sube de nivel y detecta el fin del juego.
public class ControlJuego : MonoBehaviour
{
    // Lugar donde aparece cada pieza nueva (centro arriba del tablero).
    static readonly Vector2Int PosicionInicial = new Vector2Int(4, Tablero.Alto - 2);

    // Puntos por borrar 1, 2, 3 o 4 líneas a la vez (se multiplican por el nivel).
    static readonly int[] PuntosPorLineas = { 0, 100, 300, 500, 800 };

    const int LineasPorNivel = 10;
    const float IntervaloCaidaSuave = 0.05f; // segundos entre pasos al mantener "abajo"

    // Datos que muestra la interfaz.
    public int Puntaje { get; private set; }
    public int Lineas { get; private set; }
    public int Nivel => 1 + Lineas / LineasPorNivel;
    public bool FinDelJuego { get; private set; }

    Tablero tablero;
    Pieza actual;
    float temporizadorCaida;

    public void Iniciar(Tablero tablero)
    {
        this.tablero = tablero;
        NuevaPieza();
    }

    void Update()
    {
        Keyboard teclado = Keyboard.current;
        if (teclado == null)
        {
            return; // no hay teclado conectado
        }

        // R reinicia en cualquier momento.
        if (teclado.rKey.wasPressedThisFrame)
        {
            Arranque.Reiniciar();
            return;
        }

        if (FinDelJuego)
        {
            return;
        }

        // Movimientos de un paso por cada vez que se presiona la tecla.
        if (teclado.leftArrowKey.wasPressedThisFrame) IntentarMover(Vector2Int.left);
        if (teclado.rightArrowKey.wasPressedThisFrame) IntentarMover(Vector2Int.right);
        if (teclado.upArrowKey.wasPressedThisFrame) IntentarRotar();

        // Caída automática. Si se mantiene "abajo" cae mucho más rápido (caída suave).
        bool caidaSuave = teclado.downArrowKey.isPressed;
        float intervalo = caidaSuave ? IntervaloCaidaSuave : IntervaloSegunNivel();

        temporizadorCaida += Time.deltaTime;
        if (temporizadorCaida >= intervalo)
        {
            temporizadorCaida = 0f;
            BajarUnPaso(caidaSuave);
        }

        tablero.Dibujar(actual);
    }

    // Tiempo entre pasos de caída: 0.8 s en el nivel 1 y baja con cada nivel (mínimo 0.1 s).
    float IntervaloSegunNivel()
    {
        return Mathf.Max(0.1f, 0.8f - (Nivel - 1) * 0.07f);
    }

    // Intenta mover la pieza; si la nueva posición no es válida, no hace nada.
    bool IntentarMover(Vector2Int direccion)
    {
        Pieza movida = actual.Movida(direccion);
        if (tablero.EsValida(movida))
        {
            actual = movida;
            return true;
        }
        return false;
    }

    // Intenta girar la pieza. Si choca con una pared o con otra pieza,
    // prueba recorrerla un poco a los lados (1 o 2 casillas) antes de rendirse.
    void IntentarRotar()
    {
        Pieza girada = actual.Rotada();
        int[] desplazamientos = { 0, 1, -1, 2, -2 };

        foreach (int dx in desplazamientos)
        {
            Pieza prueba = girada.Movida(new Vector2Int(dx, 0));
            if (tablero.EsValida(prueba))
            {
                actual = prueba;
                return;
            }
        }
    }

    // Baja la pieza una fila. Si ya no puede bajar, se fija y sale una nueva.
    void BajarUnPaso(bool caidaSuave)
    {
        if (IntentarMover(Vector2Int.down))
        {
            if (caidaSuave)
            {
                Puntaje += 1; // pequeño premio por bajar la pieza manualmente
            }
            return;
        }

        tablero.Fijar(actual);

        int borradas = tablero.BorrarLineasCompletas();
        if (borradas > 0)
        {
            Puntaje += PuntosPorLineas[borradas] * Nivel; // se usa el nivel de antes de sumar las líneas
            Lineas += borradas;
        }

        NuevaPieza();
    }

    // Saca una pieza al azar. Si no cabe al aparecer, el tablero está lleno: fin del juego.
    void NuevaPieza()
    {
        actual = new Pieza(Random.Range(0, FormasPiezas.Cantidad), PosicionInicial);
        temporizadorCaida = 0f;

        if (!tablero.EsValida(actual))
        {
            FinDelJuego = true;
            actual = null; // no se dibuja la pieza que ya no cupo
        }

        tablero.Dibujar(actual);
    }
}
