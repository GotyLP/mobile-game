using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementControler : MonoBehaviour 
{
    
    public Vector3 moveDir;
    public virtual Vector3 GetMovementInput()
    {
        return moveDir;
    }
    public void MoveUp()
    {
        moveDir = Vector3.forward;
    }
    public void MoveDown()
    {
        moveDir = Vector3.back;
    }
    public void MoveLeft()
    {
        moveDir = Vector3.left;
    }
    public void MoveRight()
    {
        moveDir = Vector3.right;
    }
    public void Static()
    {
        moveDir = Vector3.zero;
    }
}
    
   
   
   
    

