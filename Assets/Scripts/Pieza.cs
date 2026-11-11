using UnityEngine;

// La pieza que va cayendo: qué tipo es, dónde está y cómo está girada.
//
// Es una clase de datos sencilla (no es un MonoBehaviour). Para mover o girar
// se crea una pieza NUEVA con el cambio; así el control del juego puede
// probar el movimiento y solo aceptarlo si la nueva posición es válida.
public class Pieza
{
    public int Tipo { get; }                 // índice en FormasPiezas (0 = I, 1 = O, ...)
    public Vector2Int Posicion { get; }      // casilla del tablero donde está el centro de la pieza
    public Vector2Int[] Celdas { get; }      // las 4 casillas relativas al centro

    public Color Color => FormasPiezas.Colores[Tipo];

    // Crea una pieza recién salida, con su forma original.
    public Pieza(int tipo, Vector2Int posicion)
        : this(tipo, posicion, FormasPiezas.Formas[tipo])
    {
    }

    Pieza(int tipo, Vector2Int posicion, Vector2Int[] celdas)
    {
        Tipo = tipo;
        Posicion = posicion;
        Celdas = celdas;
    }

    // Devuelve una copia de la pieza desplazada.
    public Pieza Movida(Vector2Int desplazamiento)
    {
        return new Pieza(Tipo, Posicion + desplazamiento, Celdas);
    }

    // Devuelve una copia girada 90° en el sentido de las manecillas del reloj.
    // Girar un punto (x, y) en ese sentido da (y, -x).
    public Pieza Rotada()
    {
        if (Tipo == FormasPiezas.IndiceO)
        {
            return this; // el cuadrado no cambia al girar
        }

        Vector2Int[] giradas = new Vector2Int[Celdas.Length];
        for (int i = 0; i < Celdas.Length; i++)
        {
            giradas[i] = new Vector2Int(Celdas[i].y, -Celdas[i].x);
        }
        return new Pieza(Tipo, Posicion, giradas);
    }

    // Las 4 casillas que ocupa la pieza dentro del tablero.
    public Vector2Int[] CasillasEnTablero()
    {
        Vector2Int[] casillas = new Vector2Int[Celdas.Length];
        for (int i = 0; i < Celdas.Length; i++)
        {
            casillas[i] = Posicion + Celdas[i];
        }
        return casillas;
    }
}
