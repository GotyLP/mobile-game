using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Vector3 lastPosition;

    void Update()
    {
        Vector3 direction = transform.position - lastPosition;// Rotar hacia la direcci�n de movimiento
        if (direction.magnitude > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        lastPosition = transform.position;
    }
}
