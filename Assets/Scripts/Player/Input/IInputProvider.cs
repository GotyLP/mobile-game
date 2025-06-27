using UnityEngine;

public interface IInputProvider
{
    Vector3 GetMovementInput();
    bool IsInputActive();
    bool IsAttacking();
} 