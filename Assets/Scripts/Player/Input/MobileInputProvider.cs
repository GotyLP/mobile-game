using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputProvider : IInputProvider
{
    private Joystick _joystick;

    public MobileInputProvider(Joystick joystick)
    {        
        _joystick = joystick;
    }

    public Vector3 GetMovementInput()
    {
        if (_joystick == null) return Vector3.zero;
        
        Vector2 joystickInput = _joystick.GetMovementInput();
        return new Vector3(joystickInput.x, 0, joystickInput.y);
    }

    public bool IsInputActive()
    {
        return _joystick != null && _joystick.GetMovementInput().sqrMagnitude > 0.1f;
    }
}
