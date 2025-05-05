using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IProduct
{    public void Initialize()
    {
        Debug.Log("Enemy Initialized");
    }
}
