using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanvas : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    private void start()
    {
        if (_canvas != null)
        {
            _canvas.SetActive(true);
        }
    }
}
