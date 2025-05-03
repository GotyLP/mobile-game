using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : Controller, IDragHandler, IEndDragHandler
{
    Vector3 initialPos;
    [SerializeField] float maxMagnitude = 75; 
    void Start()
    {
        initialPos = transform.position;
    }

    public override Vector3 GetMovementInput()
    {
        Vector3 modifiedDir = new Vector3(_modeDir.x, 0, _modeDir.y);
        modifiedDir /= maxMagnitude;
        return modifiedDir;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _modeDir = Vector3.ClampMagnitude((Vector3)eventData.position - initialPos,maxMagnitude);

        transform.position = initialPos + _modeDir;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPos;
        _modeDir = Vector3.zero;
    }
   
}
