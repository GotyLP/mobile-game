using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private float maxMagnitude = 75f;
    [SerializeField] private float deadZone = 0.1f;
    
    private Vector3 initialPos;
    private Vector3 currentPos;
    private bool isDragging;

    private void Start()
    {
        initialPos = transform.position;
        currentPos = initialPos;
    }

    public Vector2 GetMovementInput()
    {
        if (!isDragging) return Vector2.zero;

        Vector2 direction = (currentPos - initialPos) / maxMagnitude;
        
        // Aplicar deadzone
        if (direction.magnitude < deadZone)
            return Vector2.zero;

        return direction;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector3 dragPosition = eventData.position;
        Vector3 moveDirection = Vector3.ClampMagnitude(dragPosition - initialPos, maxMagnitude);
        currentPos = initialPos + moveDirection;
        transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        transform.position = initialPos;
        currentPos = initialPos;
    }
}
