using UnityEngine;

public class DesktopInputProvider : IInputProvider
{
    public Vector3 GetMovementInput()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        return direction;
    }

    public bool IsInputActive()
    {
        return GetMovementInput().sqrMagnitude > 0.1f;
    }
} 