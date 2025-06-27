using UnityEngine;

public class MobileInputProvider : IInputProvider
{
    private Joystick _joystick;
    private bool _isAttacking = false;

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

    public bool IsAttacking()
    {
        // Para mobile, el ataque se controlará principalmente desde UI
        // pero mantenemos el toque como backup
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !_isAttacking)
        {
            _isAttacking = true;
        }
        else if (Input.touchCount == 0 && _isAttacking)
        {
            _isAttacking = false;
        }

        return _isAttacking;
    }

    // Métodos para ser llamados desde UI
    public void StartAttack()
    {
        _isAttacking = true;
    }

    public void StopAttack()
    {
        _isAttacking = false;
    }
}
