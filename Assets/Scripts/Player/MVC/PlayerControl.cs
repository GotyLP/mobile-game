using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerControl : Icontroller
{
    private PlayerModel _playerModel;
    Vector3 _direction;
    public PlayerControl(PlayerModel Model)
    {
        _playerModel = Model;
        _direction = Vector3.zero;
    }
    public void UpdateInputs()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerModel.TakeDamage(25f);
        }
    }
    public void FixUpdateInputs()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        _playerModel.Move(_direction);
    }
}
