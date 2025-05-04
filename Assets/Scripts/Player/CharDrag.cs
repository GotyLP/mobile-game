using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharDrag : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        float z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 pointPosition = new Vector3 (eventData.position.x, eventData.position.y, z);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pointPosition);

        //transform.position = worldPosition;
        transform.position = new Vector3 (worldPosition.x, worldPosition.y, transform.position.z);

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Estoy presionando el objeto");
    }
}
