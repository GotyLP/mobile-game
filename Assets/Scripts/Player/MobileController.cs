using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MovementController
{
    [SerializeField] private Joystick joystick;

    protected override void Awake()
    {
        base.Awake();
        if (joystick == null)
        {
            Debug.LogError("No se ha asignado el Joystick en el Inspector del MobileController. " +
                          "Arrastra el Joystick desde la jerarquía al campo 'Joystick' en el Inspector.");
        }
    }

    public override Vector3 GetMovementInput()
    {
        if (joystick == null) return Vector3.zero;
        
        // Obtener el input del joystick y convertirlo a Vector3
        Vector2 joystickInput = joystick.GetMovementInput();
        return new Vector3(joystickInput.x, 0, joystickInput.y);
    }
}
