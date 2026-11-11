using UnityEngine;

// Muestra en pantalla el puntaje, las líneas, el nivel y el mensaje de fin del juego.
// Usa OnGUI, que es la forma más sencilla de dibujar texto desde código.
public class Interfaz : MonoBehaviour
{
    ControlJuego control;

    // Estilos de texto (se crean una sola vez dentro de OnGUI).
    GUIStyle estiloMarcador;
    GUIStyle estiloMensaje;

    public void Iniciar(ControlJuego control)
    {
        this.control = control;
    }

    // Unity llama a OnGUI automáticamente cada cuadro.
    void OnGUI()
    {
        if (control == null)
        {
            return;
        }

        if (estiloMarcador == null)
        {
            estiloMarcador = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold };
            estiloMensaje = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
        }

        // El tamaño de letra depende del alto de la ventana.
        estiloMarcador.fontSize = Mathf.Max(14, Screen.height / 28);
        estiloMensaje.fontSize = Mathf.Max(18, Screen.height / 16);

        string marcador = "Puntaje: " + control.Puntaje
                        + "\nLíneas: " + control.Lineas
                        + "\nNivel: " + control.Nivel;
        GUI.Label(new Rect(15, 10, Screen.width, Screen.height), marcador, estiloMarcador);

        if (control.FinDelJuego)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Fin del juego\nPresiona R para reiniciar", estiloMensaje);
        }
    }
}
