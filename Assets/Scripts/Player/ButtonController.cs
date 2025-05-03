using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : Controller
{
    public override Vector3 GetMovementInput()
    {
        return _modeDir;
    }
    public void MoveUp()
    {
        _modeDir = Vector3.forward;
    }
    public void MoveDown()
    {
        _modeDir = Vector3.back;
    }
    public void MoveLeft()
    {
        _modeDir = Vector3.left;
    }
    public void MoveRight()
    {
        _modeDir = Vector3.right;
    }
    public void Static()
    {
        _modeDir = Vector3.zero;
    }



}
