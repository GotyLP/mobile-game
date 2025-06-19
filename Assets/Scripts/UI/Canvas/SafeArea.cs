using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class SafeArea : MonoBehaviour
{
   // [SerializeField] Canvas _myCanvas;
    [SerializeField] RectTransform _safeAreaRec;
    [SerializeField] bool _aplySafeArea;
    void OnEnable()
    {
       // if (_myCanvas == null) _myCanvas = GetComponent<Canvas>();
       if(_safeAreaRec == null) _safeAreaRec = GetComponent<RectTransform>();

        if (_aplySafeArea) AdjustRect();

        //_canvas.rederMode = RenderMode.ScreenSpaceCamera;
        //_canvas.worldCamera = Camera.Main;
    }
    private void AdjustRect()
    {
        if (_safeAreaRec == null) return;

        Rect lastSafeArea = Screen.safeArea;

        Vector2 minAnchor = lastSafeArea.position;
        Vector2 maxAnchor = minAnchor + lastSafeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        _safeAreaRec.anchorMin = minAnchor;
        _safeAreaRec.anchorMax = maxAnchor;
    }
}
