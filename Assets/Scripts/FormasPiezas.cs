using UnityEngine;

// Datos de las 7 piezas clásicas: su forma y su color.
//
// Cada forma es una lista de 4 casillas relativas a un punto central (0, 0),
// que es el punto alrededor del cual gira la pieza.
// x crece hacia la derecha, y crece hacia ARRIBA.
public static class FormasPiezas
{
    public static readonly string[] Nombres = { "I", "O", "T", "S", "Z", "J", "L" };

    public static readonly Vector2Int[][] Formas =
    {
        // I:  ■ ■ ■ ■
        new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) },

        // O:  ■ ■
        //     ■ ■
        new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },

        // T:    ■
        //     ■ ■ ■
        new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1) },

        // S:    ■ ■
        //     ■ ■
        new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },

        // Z:  ■ ■
        //       ■ ■
        new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },

        // J:  ■
        //     ■ ■ ■
        new[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },

        // L:      ■
        //     ■ ■ ■
        new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1) },
    };

    // Un color por pieza, en el mismo orden que Formas.
    public static readonly Color[] Colores =
    {
        new Color(0.2f, 0.85f, 0.95f), // I: cian
        new Color(0.95f, 0.85f, 0.2f), // O: amarillo
        new Color(0.65f, 0.3f, 0.9f),  // T: morado
        new Color(0.3f, 0.85f, 0.35f), // S: verde
        new Color(0.9f, 0.25f, 0.25f), // Z: rojo
        new Color(0.25f, 0.4f, 0.95f), // J: azul
        new Color(0.95f, 0.55f, 0.15f),// L: naranja
    };

    // La pieza O es un cuadrado: girarla no cambia nada, así que no rota.
    public const int IndiceO = 1;

    public static int Cantidad => Formas.Length;
}
