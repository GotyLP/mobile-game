using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobileController : Joystick
{
    public Joystick joystick;
    public float mobileSpeed = 5f;
    void Update()
    {
        transform.position += joystick.GetMovementInput() * mobileSpeed * Time.deltaTime;        
    }
}
