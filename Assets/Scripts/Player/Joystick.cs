using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Joystick))]

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] Vector3 _moveDir;
    Vector3 initialPos;
    [SerializeField] float maxMagnitude = 75; 
    void Start()
    {
        initialPos = transform.position;
    }

    public new Vector3 GetMovementInput()
    {
        Vector3 modifiedDir = new Vector3(_moveDir.x, 0, _moveDir.y);
        modifiedDir /= maxMagnitude;
        return modifiedDir;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - initialPos,maxMagnitude);

        transform.position = initialPos + _moveDir;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPos;
        _moveDir = Vector3.zero;
    }
   
}
