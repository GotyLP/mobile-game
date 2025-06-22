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

    public PlayerController(Player user)
    {
        _playerModel = user.Model;
        _direction = Vector3.zero;
        
        // Configurar el proveedor de input según la plataforma
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
        // Manejar input de ataque
        if (_inputProvider.IsAttacking())
        {
            _playerModel.Attack();
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

    private void HandleAttackInput()
    {
        // Input de ataque para diferentes plataformas
        bool attackInput = false;
        
        if (Application.isMobilePlatform)
        {
            // Para móvil, podrías usar un botón de ataque específico
            // Por ahora, uso un toque en cualquier parte como ejemplo
            attackInput = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
        else
        {
            // Para escritorio, usar clic izquierdo
            attackInput = Input.GetMouseButtonDown(0);
        }

        if (attackInput && !_isAttacking)
        {
            _isAttacking = true;
            _playerModel.Attack();
        }
        else if (!attackInput && _isAttacking)
        {
            _isAttacking = false;
            _playerModel.StopAttack();
        }
    }

    // Método para obtener el InputProvider actual (útil para UI)
    public IInputProvider GetInputProvider()
    {
        return _inputProvider;
    }

    // Método para cambiar el proveedor de input dinámicamente si es necesario
    public void SetInputProvider(IInputProvider newProvider)
    {
        _inputProvider = newProvider;
    }
}


