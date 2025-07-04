using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyExample : MonoBehaviour
{
    private Rigidbody _rgbd;

    private Vector3 _currentVelocity;

    private void Awake()
    {
        _rgbd = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rgbd.isKinematic = false;
        
        _rgbd.velocity = _currentVelocity;
    }

    private void OnDisable()
    {
        _currentVelocity = _rgbd.velocity;
        
        _rgbd.isKinematic = true;
    }
}
