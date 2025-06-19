using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour 
{
    [SerializeField] protected float moveSpeed = 5f;
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
        //rb.freezeRotation = true;
    }

    public virtual Vector3 GetMovementInput()
    {
        return moveDirection;
    }

    public virtual void MoveUp()
    {
        //moveDirection = Vector3.forward;
        rb.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    public virtual void MoveDown()
    {
       // moveDirection = Vector3.back;
        rb.position += Vector3.back * moveSpeed * Time.deltaTime;
    }

    public virtual void MoveLeft()
    {
        //moveDirection = Vector3.left;
        rb.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    public virtual void MoveRight()
    {
        //moveDirection = Vector3.right;
        rb.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public virtual void Stop()
    {
        moveDirection = Vector3.zero;
    }

    protected virtual void Update()
    {
        if (rb != null)
        {
            Vector3 movement = GetMovementInput() * moveSpeed;
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    public abstract Vector3 GetMovementInput();

    //public virtual void MoveUp()
    //{
    //    moveDirection = Vector3.forward;
    //}

    //public virtual void MoveDown()
    //{
    //    moveDirection = Vector3.back;
    //}

    //public virtual void MoveLeft()
    //{
    //    moveDirection = Vector3.left;
    //}

    //public virtual void MoveRight()
    //{
    //    moveDirection = Vector3.right;
    //}

    //public virtual void Stop()
    //{
    //    moveDirection = Vector3.zero;
    //}
}
