using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopScreenOnClick : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        
        button.onClick.AddListener(Execute);
    }

    void Execute()
    {
        ScreenManager.Instance.Pop();
    }
    
}
