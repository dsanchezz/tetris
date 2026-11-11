using UnityEngine;

// Crea por código el único sprite del juego: un cuadro blanco de 1x1 unidades.
// No se usan imágenes externas; el color de cada cuadro se pone con SpriteRenderer.color.
public static class GeneradorSprites
{
    // Se guarda para no crear la textura cada vez que se pide.
    static Sprite cuadro;

    public static Sprite Cuadro()
    {
        if (cuadro == null)
        {
            const int tam = 16;
            Texture2D textura = new Texture2D(tam, tam);
            textura.filterMode = FilterMode.Point; // bordes nítidos

            // Todos los píxeles en blanco.
            Color[] pixeles = new Color[tam * tam];
            for (int i = 0; i < pixeles.Length; i++)
            {
                pixeles[i] = Color.white;
            }
            textura.SetPixels(pixeles);
            textura.Apply();

            // pixelsPerUnit = tam  ->  el sprite mide exactamente 1 unidad.
            cuadro = Sprite.Create(textura, new Rect(0, 0, tam, tam), new Vector2(0.5f, 0.5f), tam);
        }
        return cuadro;
    }

    // Atajo: crea un GameObject con un SpriteRenderer de cuadro ya configurado.
    public static SpriteRenderer CrearCuadro(string nombre, Color color, int orden, Transform padre)
    {
        GameObject objeto = new GameObject(nombre);
        objeto.transform.SetParent(padre, false);

        SpriteRenderer render = objeto.AddComponent<SpriteRenderer>();
        render.sprite = Cuadro();
        render.color = color;
        render.sortingOrder = orden; // los números mayores se dibujan encima

        return render;
    }
}
