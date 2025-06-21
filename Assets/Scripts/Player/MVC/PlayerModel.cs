using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerMVC
{
    public class PlayerModel
    {
        Rigidbody _rb;
        IController _controller;
        float _speed;
        
        private float _currentLife;
        private float _maxLife;

        public event Action<float, float> OnMovement = delegate { };

        public float CurrentLife => _currentLife;
        public float MaxLife => _maxLife;

        public PlayerModel(Player user)
        {
            _rb = user.Rigidbody;
            _speed = user.Speed;
            _maxLife = user.StartLife;
            _currentLife = _maxLife;
        }

        public void Move(Vector3 direction)
        {
            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
            }
            _rb.MovePosition(_rb.position + direction * (_speed * Time.fixedDeltaTime));
            OnMovement(direction.x, direction.z);
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

        public void Heal(float amount)
        {
            _currentLife += amount;
            _currentLife = Mathf.Min(_maxLife, _currentLife); // Avoid exceeding max life
            EventManager.Trigger(new PlayerHealthChangedEvent(_currentLife, _maxLife));
        }
    }
}
   
