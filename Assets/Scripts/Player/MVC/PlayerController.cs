using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace PlayerMVC
{    
    public class PlayerController : IController
    {
        private PlayerModel _playerModel;
        private IInputProvider _inputProvider;
        private Vector3 _direction;

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
                    Debug.LogWarning("MobileController no encontrado en el Player para plataforma móvil");
                    _inputProvider = new DesktopInputProvider();
                }
            }
            else
            {
                _inputProvider = new DesktopInputProvider();
                if (_inputProvider == null)
                {
                    _inputProvider = new DesktopInputProvider();
                }
            }

        }

        public void UpdateInputs()
        {
            
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

        public void SetInputProvider(IInputProvider newProvider)
        {
            _inputProvider = newProvider;
        }
    }
}

