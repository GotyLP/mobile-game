using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView
{
    Animator _animator;
    
    public PlayerView(Player user)
    {
        _animator = user.Animator;

        var model = user.Model;
        if (model != null ) return;
        model.OnMovement += MovementAnimation;
    }
    private void MovementAnimation(float xValue, float yValue)
    {
        //_animator.SetFloat("xAxi", xValue);
        //_animator.SetFloat("yAxi", yValue);
    }
}

