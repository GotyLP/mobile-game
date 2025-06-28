using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public class PlayerView
{
    Animator _animator;
    public float HorizontalAnimSmoothTime = 0.2f;

    public float VerticalAnimTime = 0.2f;

    public float StartAnimTime = 0.3f;

    public float StopAnimTime = 0.15f;

    public float allowPlayerRotation = 0.1f;

    public float Speed;
    public PlayerView(Player user)
    {
        _animator = user.Animator;
        //animator.GetBehaviours<BreathBehaviour>();
        var model = user.Model;
        if (model == null) 
        {
            Debug.LogError("PlayerView: Model is null, cannot subscribe to movement events");
            return;
        }
        
        model.OnMovement += MovementAnimation;
        Debug.Log("PlayerView: Suscrito al evento OnMovement");
    }
    
    private void MovementAnimation(float xValue, float zValue)
    {
        Debug.Log($"MovementAnimation ejecutándose - X: {xValue:F2}, Z: {zValue:F2}");
        _animator.SetFloat("InputZ", zValue, VerticalAnimTime, Time.fixedDeltaTime);
        _animator.SetFloat("InputX", xValue, HorizontalAnimSmoothTime, Time.fixedDeltaTime);

        //Calculate the Input Magnitude
        Speed = new Vector2(xValue, zValue).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            Debug.Log($"Activando animación WALK - Speed: {Speed:F3}");
            _animator.SetFloat("InputMagnitude", Speed, StartAnimTime, Time.fixedDeltaTime);
        }
        else if (Speed < allowPlayerRotation)
        {
            Debug.Log($"Activando animación IDLE - Speed: {Speed:F3}");
            _animator.SetFloat("InputMagnitude", Speed, StopAnimTime, Time.fixedDeltaTime);
        }
    }
}

