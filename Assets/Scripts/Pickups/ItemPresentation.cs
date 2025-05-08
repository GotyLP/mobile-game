using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPresentation : MonoBehaviour
{
    public float rotationSpeed = 20f;     
    public float floatAmplitude = 0.5f;    
    public float floatFrequency = 1f;      

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
