using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDebugger : MonoBehaviour
{
   

    [SerializeField] private SimpleUiScreen _pauseScreen;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenManager.Instance.Push(_pauseScreen);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScreenManager.Instance.Pop();
        }
    }
    public void Pause()
    {
        ScreenManager.Instance.Push(_pauseScreen);
    }
    public void Continue()
    {
        ScreenManager.Instance.Pop();
    }
}