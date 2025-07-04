using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SimpleUiScreen : MonoBehaviour, IScreen
{
    private Button[] _buttons;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    void Start()
    {
        _buttons = GetComponentsInChildren<Button>();
    }

    void ActivateButtons(bool boolValue)
    {
        foreach (var button in _buttons)
        {
            button.interactable = boolValue;
        }
    }
    
    public void Activate()
    {
        ActivateButtons(true);

        _canvas.enabled = true;
    }

    public void Deactivate()
    {
        ActivateButtons(false);

        _canvas.enabled = false;
    }

    public void Release()
    {
        _canvas.enabled = false;
    }
}
