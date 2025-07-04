using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : MonoBehaviour, IScreen
{
    List<Behaviour> _previousStates;
    
    private void Awake()
    {
        _previousStates = new List<Behaviour>();
    }

    private void Start()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.green;
        }
    }

    public void Activate()
    {
        foreach (var behaviour in _previousStates)
        {
            behaviour.enabled = true;
        }
        
        _previousStates.Clear();
        
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.green;
        }
    }

    public void Deactivate()
    {
        foreach (var behaviour in GetComponentsInChildren<Behaviour>())
        {
            if (!behaviour.enabled) continue;
            
            _previousStates.Add(behaviour);
            
            behaviour.enabled = false;
        }

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.red;
        }
        
    }

    public void Release()
    {
        Destroy(gameObject);
    }
}
