using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System;


public class PlayerView
{
    Animator _animator;
    public float HorizontalAnimSmoothTime = 0.2f;

    public float VerticalAnimTime = 0.2f;

    public float StartAnimTime = 0.3f;

    public float StopAnimTime = 0.15f;

    public float allowPlayerRotation = 0.1f;

    public float Speed;
    
    public event Action OnAttackTriggered = delegate { };
    
    public PlayerView(Player user)
    {
        _animator = user.Animator;
        var model = user.Model;
        if (model == null) 
        {
            Debug.LogError("PlayerView: Model is null, cannot subscribe to movement events");
            return;
        }
        
        model.OnMovement += MovementAnimation;
    }
    
    private void MovementAnimation(float xValue, float zValue)
    {
        _animator.SetFloat("InputZ", zValue, VerticalAnimTime, Time.fixedDeltaTime);
        _animator.SetFloat("InputX", xValue, HorizontalAnimSmoothTime, Time.fixedDeltaTime);

        Speed = new Vector2(xValue, zValue).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            _animator.SetFloat("InputMagnitude", Speed, StartAnimTime, Time.fixedDeltaTime);
        }
        else if (Speed < allowPlayerRotation)
        {
            _animator.SetFloat("InputMagnitude", Speed, StopAnimTime, Time.fixedDeltaTime);
        }
    }
    
 
    public void TriggerAttackAnimation(string animationTrigger = "Attack01")
    {
        if (_animator != null)
        {
            _animator.SetTrigger(animationTrigger);
            Debug.Log($"PlayerView: Triggered {animationTrigger} animation");
                
            OnAttackTriggered?.Invoke();
        }
        else
        {
            Debug.LogError("PlayerView: Animator is null, cannot trigger attack animation");
        }
    }
    
   
    public bool IsAttacking()
    {
        if (_animator != null)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(1); // Layer "Attack" es el index 1
            return stateInfo.IsTag("Attack");
        }
        return false;
    }
    
   
    public float GetAttackProgress()
    {
        if (_animator != null && IsAttacking())
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(1);
            return stateInfo.normalizedTime;
        }
        return 0f;
    }
}

