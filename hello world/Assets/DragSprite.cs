using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSprite : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;
    private bool isDragging = false;
    private Vector2 lastPosition;
    private Animator Animator;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        Animator = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Convierte la posición del mouse a coordenadas locales del canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition
        );

        // Guarda el desplazamiento entre el mouse y el objeto
        offset = rectTransform.anchoredPosition - localPointerPosition;
        isDragging = true;
        lastPosition = localPointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // Convierte la nueva posición del mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition
        );

        // Ajusta la posición del objeto respetando el offset
        rectTransform.anchoredPosition = localPointerPosition + offset;

        // Determina la dirección del arrastre
        Vector2 direction = localPointerPosition - lastPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                Animator.SetFloat("X", 1);
                Animator.SetFloat("Y", 0);
            }
            else
            {
                Animator.SetFloat("X", -1);
                Animator.SetFloat("Y", 0);

            }
        }
        else
        {
            if (direction.y > 0)
            {
                Animator.SetFloat("Y", 1);
                Animator.SetFloat("X", 0);
            }
            else
            {
                Animator.SetFloat("Y", -1);
                Animator.SetFloat("X", 0);
            }
        }

        lastPosition = localPointerPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
