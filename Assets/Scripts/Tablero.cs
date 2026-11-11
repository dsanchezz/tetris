using UnityEngine;

// El tablero de 10 x 20 casillas.
// Guarda qué casillas están ocupadas por piezas ya fijas, revisa si una pieza
// cabe en un lugar, borra líneas completas y dibuja todo en pantalla.
//
// Coordenadas: x = columna (0 a la izquierda), y = fila (0 ABAJO, 19 arriba).
public class Tablero : MonoBehaviour
{
    public const int Ancho = 10;
    public const int Alto = 20;

    const int Vacia = -1; // valor de una casilla sin pieza

    static readonly Color ColorFondo = new Color(0.35f, 0.35f, 0.4f); // marco alrededor del tablero
    static readonly Color ColorVacia = new Color(0.08f, 0.08f, 0.1f); // casilla vacía

    // Qué hay en cada casilla: Vacia o el tipo de pieza que la ocupa.
    int[,] casillas;

    // Un cuadro dibujado por casilla; solo se les cambia el color.
    SpriteRenderer[,] cuadros;

    // Crea los datos y los 200 cuadros que se ven en pantalla.
    public void Construir()
    {
        casillas = new int[Ancho, Alto];
        cuadros = new SpriteRenderer[Ancho, Alto];

        // Un rectángulo un poco más grande que el tablero sirve de marco.
        SpriteRenderer fondo = GeneradorSprites.CrearCuadro("Marco", ColorFondo, 0, transform);
        fondo.transform.localScale = new Vector3(Ancho + 0.4f, Alto + 0.4f, 1f);

        for (int x = 0; x < Ancho; x++)
        {
            for (int y = 0; y < Alto; y++)
            {
                casillas[x, y] = Vacia;

                SpriteRenderer cuadro = GeneradorSprites.CrearCuadro("Casilla " + x + "," + y, ColorVacia, 1, transform);
                cuadro.transform.position = AMundo(x, y);
                cuadro.transform.localScale = Vector3.one * 0.92f; // deja una rayita entre casillas
                cuadros[x, y] = cuadro;
            }
        }
    }

    // ¿La pieza cabe donde está? (dentro del tablero y sin chocar con piezas fijas)
    public bool EsValida(Pieza pieza)
    {
        foreach (Vector2Int c in pieza.CasillasEnTablero())
        {
            bool fuera = c.x < 0 || c.x >= Ancho || c.y < 0 || c.y >= Alto;
            if (fuera || casillas[c.x, c.y] != Vacia)
            {
                return false;
            }
        }
        return true;
    }

    // Deja la pieza fija en el tablero (cuando ya no puede bajar más).
    public void Fijar(Pieza pieza)
    {
        foreach (Vector2Int c in pieza.CasillasEnTablero())
        {
            casillas[c.x, c.y] = pieza.Tipo;
        }
    }

    // Borra todas las filas llenas, baja lo de arriba y devuelve cuántas borró.
    public int BorrarLineasCompletas()
    {
        int borradas = 0;
        int y = 0;
        while (y < Alto)
        {
            if (FilaLlena(y))
            {
                BajarFilasDesde(y);
                borradas++;
                // No se avanza "y": la fila que bajó a este lugar también hay que revisarla.
            }
            else
            {
                y++;
            }
        }
        return borradas;
    }

    bool FilaLlena(int y)
    {
        for (int x = 0; x < Ancho; x++)
        {
            if (casillas[x, y] == Vacia)
            {
                return false;
            }
        }
        return true;
    }

    // Cada fila arriba de "filaBorrada" baja una posición; la de hasta arriba queda vacía.
    void BajarFilasDesde(int filaBorrada)
    {
        for (int y = filaBorrada; y < Alto - 1; y++)
        {
            for (int x = 0; x < Ancho; x++)
            {
                casillas[x, y] = casillas[x, y + 1];
            }
        }
        for (int x = 0; x < Ancho; x++)
        {
            casillas[x, Alto - 1] = Vacia;
        }
    }

    // Pinta las casillas fijas y, encima, la pieza que va cayendo (si hay una).
    public void Dibujar(Pieza piezaActual)
    {
        for (int x = 0; x < Ancho; x++)
        {
            for (int y = 0; y < Alto; y++)
            {
                int tipo = casillas[x, y];
                cuadros[x, y].color = tipo == Vacia ? ColorVacia : FormasPiezas.Colores[tipo];
            }
        }

        if (piezaActual != null)
        {
            foreach (Vector2Int c in piezaActual.CasillasEnTablero())
            {
                cuadros[c.x, c.y].color = piezaActual.Color;
            }
        }
    }

    // Convierte una casilla en posición del mundo; el tablero queda centrado en (0, 0).
    public static Vector3 AMundo(int x, int y)
    {
        return new Vector3(x - (Ancho - 1) / 2f, y - (Alto - 1) / 2f, 0f);
    }
}
