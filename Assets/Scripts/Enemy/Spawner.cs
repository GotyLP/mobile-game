using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _factory.GetProduct(Vector3.zero);
        }
    }
}
