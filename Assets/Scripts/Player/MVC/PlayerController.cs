using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : IController
{
    private PlayerModel _playerModel;
    private IInputProvider _inputProvider;
    private Vector3 _direction;
    private bool _isAttacking;
    private bool _uiAttackMode = false; // Flag para saber si estamos usando ataques de UI

    public PlayerController(Player user)
    {
        _playerModel = user.Model;
        _direction = Vector3.zero;
        
        SetupInputProvider(user);
    }

    private void SetupInputProvider(Player user)
    {
        if (Application.isMobilePlatform)
        {
            _inputProvider = new MobileInputProvider(user.Joystick);
            if (_inputProvider == null)
            {
                Debug.LogWarning("MobileInputProvider no pudo ser creado, usando DesktopInputProvider como fallback");
                _inputProvider = new DesktopInputProvider();
            }
        }
        else
        {
            _inputProvider = new DesktopInputProvider();
        }
    }

    public void UpdateInputs()
    {
        if (!_uiAttackMode)
        {
            HandleAutomaticAttackInput();
        }
    }

    private void HandleAutomaticAttackInput()
    {
        bool currentlyAttacking = _inputProvider.IsAttacking();
        
        if (currentlyAttacking && !_isAttacking)
        {
            _isAttacking = true;
            Debug.Log("PlayerController: Iniciando ataque automático");
            _playerModel.Attack();
        }
        else if (!currentlyAttacking && _isAttacking)
        {
            _isAttacking = false;
            Debug.Log("PlayerController: Deteniendo ataque automático");
            _playerModel.StopAttack();
        }
    }

    public void FixUpdateInputs()
    {
        if (_inputProvider != null)
        {
            _direction = _inputProvider.GetMovementInput();
            
            if (_inputProvider.IsInputActive())
            {
                _playerModel.Move(_direction);
            }
        }
    }
#region UI Attack Mode
    public void EnableUIAttackMode()
    {
        _uiAttackMode = true;
        Debug.Log("PlayerController: Modo ataque UI activado");
    }

    public void DisableUIAttackMode()
    {
        _uiAttackMode = false;
        Debug.Log("PlayerController: Modo ataque UI desactivado");
    }
#endregion

    public IInputProvider GetInputProvider()
    {
        return _inputProvider;
    }

    public void SetInputProvider(IInputProvider newProvider)
    {
        _inputProvider = newProvider;
    }
}


