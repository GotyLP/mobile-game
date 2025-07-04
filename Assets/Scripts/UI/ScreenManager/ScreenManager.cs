using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    private Stack<IScreen> _screensStack;
    
    private void Awake()
    {
        Instance = this;

        _screensStack = new Stack<IScreen>();
    }

    public void Push(IScreen newScreen) //agrega una pantalla a la pila de pantallas
    {
        if (_screensStack.Contains(newScreen)) return;

        if (_screensStack.Count != 0)
        {
            var oldScreen = _screensStack.Peek();
            oldScreen.Deactivate();
        }

        _screensStack.Push(newScreen);
        newScreen.Activate();
    }
    
    public void Pop() //elimina la ultima pantalla
    {
        if (_screensStack.Count <= 1) return;

        var screenToPop = _screensStack.Pop();
        screenToPop.Release();

        var lastScreen = _screensStack.Peek();
        lastScreen.Activate();
    }
}