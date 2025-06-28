using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerModel
{
    Rigidbody _rb;
    IController _controller;
    [SerializeField] float _speed;   

    [SerializeField] private float _currentLife;
    public float _maxLife;
    private AttackSystem _attackSystem;
    private Inventory _inventory;
    private CharacterController _characterController;
    private Transform _transform;

    private Player _player;


    // Movement and rotation variables
    private Vector3 _velocity;
    private float _gravity = -9.81f;
    private float _rotationSpeed = 10f;
    private Vector3 _lastMovementDirection;

    public event Action<float, float> OnMovement = delegate { };   


    public float CurrentLife => _currentLife;
    public float MaxLife => _maxLife;
    public PlayerModel(Player user)
    {
        _player = user;
        _rb = user.Rigidbody;
        _speed = user.Speed;
        _maxLife = user.StartLife;
        _currentLife = _maxLife;
        _attackSystem = user.AttackSystem;
        _inventory = user.Inventory;
        _characterController = user.CharacterController;
        _transform = user.transform;
        
        // Initialize velocity
        _velocity = Vector3.zero;
        Debug.Log("PlayerModel initialized with current Life: " + _currentLife);
    }    
    public void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        Vector3 move = direction * _speed;
        
        if (direction.sqrMagnitude > 0.1f)
        {
            _lastMovementDirection = direction;
            
            Rotate(_lastMovementDirection);
        }
       
        if (!_characterController.isGrounded)
        {
            _velocity.y += _gravity * Time.fixedDeltaTime;
        }
        else
        {
            if (_velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        }

     
        Vector3 finalMovement = move + Vector3.up * _velocity.y;
        
        _characterController.Move(finalMovement * Time.fixedDeltaTime);
        
        OnMovement(direction.x, direction.z);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void Attack()
    {
        if (_inventory != null && _inventory.GetCurrentWeaponData() != null)
        {
            _attackSystem?.ExecuteAttack(_inventory.GetCurrentWeaponData());
        }
        else
        {
            Debug.LogWarning("No hay arma equipada para atacar");
        }
    }

    public void StopAttack()
    {
        if (_inventory != null && _inventory.GetCurrentWeaponData() != null)
        {
            _attackSystem?.StopAttack(_inventory.GetCurrentWeaponData());
        }
    }

    public void TakeDamage(float damage)
    {        
        _currentLife -= damage;
        _currentLife = Mathf.Max(0, _currentLife); // Avoid negative life
        
        EventManager.Trigger(new PlayerHealthChangedEvent(_currentLife, _maxLife));          
        
        if (_currentLife <= 0)  
        {
            EventManager.Trigger(SimpleEventType.PlayerDeathEvent);            
        }
    }
    public void AddSpeed()
    {
        _speed += 5;
        Debug.Log($"Speed increased to {_speed}");
    }
    public void AddLife()
    {
        _maxLife += 50;
        _currentLife = MaxLife;
        Debug.Log($"Max life increased to {_maxLife}");
        //_currentLife = Mathf.Min(_maxLife, _currentLife); // Avoid exceeding max life       
    }

    public void Heal(float amount)
    {
        _currentLife += amount;
        _currentLife = Mathf.Min(_maxLife, _currentLife); // Avoid exceeding max life
        EventManager.Trigger(new PlayerHealthChangedEvent(_currentLife, _maxLife));
    }

    public void SetGravity(float gravity)
    {
        _gravity = gravity;
    }

    public void SetRotationSpeed(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }
}

   
