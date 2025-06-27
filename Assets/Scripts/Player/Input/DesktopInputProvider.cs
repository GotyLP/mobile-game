using UnityEngine;

public class DesktopInputProvider : IInputProvider
{
    private bool _isAttacking = false;

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

    public bool IsAttacking()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            _isAttacking = true;
        }
        else if (Input.GetMouseButtonUp(0) && _isAttacking)
        {
            _isAttacking = false;
        }

        return _isAttacking;
    }
} 