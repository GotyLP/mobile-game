using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    Camera _myCam;
    
    [SerializeField, Range(0.1f, 2f)] float _zoomSpeed = 0.5f;
    private void Start() => _myCam = Camera.main;
    void Update()
    {
        if (Input.touchCount != 2) return;
        {
            Touch firstTouch = Input.touches[0];
            Touch secondTouch = Input.touches[1];

            Vector2 currentTouchMagnitudes = firstTouch.position - secondTouch.position;

            Vector2 firstTouchLastPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchLastPos = secondTouch.position - secondTouch.deltaPosition;


            Vector2 deltaTouchMagnitudes = firstTouchLastPos - secondTouchLastPos;

            float zoom = currentTouchMagnitudes.magnitude - deltaTouchMagnitudes.magnitude;

            if (zoom != 0)
            {
                if (_myCam.orthographic) _myCam.orthographicSize = Mathf.Clamp
                        (_myCam.orthographicSize + cameraZoomCalculation(zoom), 2, 8);

                else _myCam.fieldOfView = Mathf.Clamp(_myCam.fieldOfView + cameraZoomCalculation
                    (zoom), 20, 60);
            }
        }
        float CameraZoomCalculation(float zoom)
        {
            return zoom * _zoomSpeed * Time.deltaTime;
        }
        float cameraZoomCalculation(float zoom) => zoom * _zoomSpeed * Time.deltaTime;

    }
    
}
//if (Input.touchCount != 2) return;
//{
//    Touch firstTouch = Input.touches[0];
//    Touch secondTouch = Input.touches[1];

//    Vector2 firstTouchLastPos = firstTouch.position - firstTouch.deltaPosition;//Final- Inicial
//    Vector2 secondTouchLastPos = secondTouch.position - secondTouch.deltaPosition;

//    Vector2 initialTouchPos = firstTouch.position - secondTouch.position;
//    Vector2 finalToucPos = firstTouchLastPos - secondTouchLastPos;

//    float zoom = initialTouchPos.magnitude - finalToucPos.magnitude;

//    if (zoom != 0)
//    {
//        if (Camera.main.orthographic) Camera.main.orthographicSize += zoom * _zoomSpeed * Time.deltaTime ;
//        else Camera.main.fieldOfView += zoom * _zoomSpeed * Time.deltaTime;


//    }
