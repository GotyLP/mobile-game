using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour 
{
    protected Vector3 moveDirection;
    [SerializeField] protected float moveSpeed = 5f;  // Valor por defecto pero no serializado
    protected Rigidbody rb;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.freezeRotation = true;
    }

    public virtual Vector3 GetMovementInput()
    {
        return moveDirection;
    }

    public virtual void MoveUp()
    {
        moveDirection = Vector3.forward;
    }

    public virtual void MoveDown()
    {
        moveDirection = Vector3.back;
    }

    public virtual void MoveLeft()
    {
        moveDirection = Vector3.left;
    }

    public virtual void MoveRight()
    {
        moveDirection = Vector3.right;
    }

    public virtual void Stop()
    {
        moveDirection = Vector3.zero;
    }

    protected virtual void Update()
    {
        // Calcular el movimiento
        Vector3 movement = GetMovementInput() * moveSpeed;
        
        // Aplicar el movimiento al Rigidbody
        if (rb != null)
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
    }
}
