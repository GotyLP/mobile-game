using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MovementControler
{
    public float moveSpeed = 5f;
    private void Update()
    {
        //Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
            Debug.Log("W");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        //transform.Translate(movement * moveSpeed * Time.deltaTime);
    }





    //public float speed = 5f;
    //private Vector3 lastPosition;

    //void Start()
    //{
    //    lastPosition = transform.position;
    //}

    //void Update()
    //{

    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");
    //    Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

    //    transform.position += movement * speed * Time.deltaTime;
    //}
}
