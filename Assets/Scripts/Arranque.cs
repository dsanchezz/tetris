using UnityEngine;

// Punto de entrada del juego.
// No hace falta poner nada en la escena: al dar Play, Unity llama
// automáticamente a AlIniciar() gracias a [RuntimeInitializeOnLoadMethod]
// y desde ahí se crea todo por código.
public static class Arranque
{
    // Objeto raíz que contiene todo el juego; al reiniciar se destruye y se vuelve a crear.
    static GameObject raiz;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AlIniciar()
    {
        ConfigurarCamara();
        CrearJuego();
    }

    // Borra la partida actual y crea una nueva (tecla R).
    public static void Reiniciar()
    {
        if (raiz != null)
        {
            Object.Destroy(raiz);
        }
        CrearJuego();
    }

    static void CrearJuego()
    {
        raiz = new GameObject("Juego");

        // 1) Tablero de 10 x 20 con sus casillas dibujadas.
        Tablero tablero = raiz.AddComponent<Tablero>();
        tablero.Construir();

        // 2) Control de la partida (teclado, caída, puntaje, nivel, fin del juego).
        ControlJuego control = raiz.AddComponent<ControlJuego>();
        control.Iniciar(tablero);

        // 3) Interfaz con puntaje, líneas y nivel.
        Interfaz interfaz = raiz.AddComponent<Interfaz>();
        interfaz.Iniciar(control);
    }

    // Cámara ortográfica centrada en el tablero.
    static void ConfigurarCamara()
    {
        // La escena de la plantilla ya trae una "Main Camera"; si no existiera, se crea una.
        Camera camara = Camera.main;
        if (camara == null)
        {
            GameObject objetoCamara = new GameObject("Main Camera");
            objetoCamara.tag = "MainCamera";
            camara = objetoCamara.AddComponent<Camera>();
        }

        camara.orthographic = true;
        camara.transform.position = new Vector3(0f, 0f, -10f);
        camara.clearFlags = CameraClearFlags.SolidColor;
        camara.backgroundColor = Color.black;

        // orthographicSize es la mitad de lo que se ve en vertical: 20 filas / 2 + un margen.
        camara.orthographicSize = Tablero.Alto / 2f + 1f;
    }
}
