using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetalleItem : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public float offset = 10f;

    private void Update()
    {
        Vector2 cursorPosition = Input.mousePosition;
        Vector2 panelPosition = cursorPosition;

        // Verificar si el panel sobresale de la pantalla hacia la derecha
        float panelRightEdge = panelPosition.x + panelRectTransform.rect.width;
        float screenRightEdge = Screen.width;
        if (panelRightEdge > screenRightEdge)
        {
            panelPosition.x += -offset;
        }
        else {
            panelPosition.x += offset;
        }

        panelRectTransform.position = panelPosition;
    }
}
