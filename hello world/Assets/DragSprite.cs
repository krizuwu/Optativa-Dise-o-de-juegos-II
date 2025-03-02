using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSprite : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;
    private bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Convierte la posici�n del mouse a coordenadas locales del canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition
        );

        // Guarda el desplazamiento entre el mouse y el objeto
        offset = rectTransform.anchoredPosition - localPointerPosition;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // Convierte la nueva posici�n del mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition
        );

        // Ajusta la posici�n del objeto respetando el offset
        rectTransform.anchoredPosition = localPointerPosition + offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}