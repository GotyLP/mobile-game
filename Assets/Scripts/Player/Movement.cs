using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MovementControler
{
    public float dskSpeed = 5f;
    private void Update()
    {

    }
    private void OnMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        transform.Translate(moveDir * dskSpeed * Time.deltaTime);
    }

}
