using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementControler : MonoBehaviour 
{
   public Vector3 modeDir;
    public virtual Vector3 GetMovementInput()
    {
        return modeDir;
    }
    public void MoveUp()
    {
        modeDir = Vector3.forward;
    }
    public void MoveDown()
    {
        modeDir = Vector3.back;
    }
    public void MoveLeft()
    {
        modeDir = Vector3.left;
    }
    public void MoveRight()
    {
        modeDir = Vector3.right;
    }
    public void Static()
    {
        modeDir = Vector3.zero;
    }



}
