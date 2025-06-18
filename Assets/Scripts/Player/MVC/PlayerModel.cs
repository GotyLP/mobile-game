using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    Rigidbody _rb;
    Icontroller _controler;
    [SerializeField] float _startLife;
    [SerializeField] float _speed;
    float _currentLife;

    public event Action<float, float> OnMovement = delegate { };

    public event Action<float> OnLifeChange = delegate { };

    public event Action OnDead = delegate { };
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controler = new PlayerControl(this);
        _currentLife = _startLife;
    }
    private void Update()
    {
        _controler.UpdateInputs();
    }
    private void FixedUpdate()
    {
        _controler.FixUpdateInputs();
    }
    public void TakeDamage(float damage)
    {
        _currentLife -= damage;
        if (_currentLife < 0)
        {
            Dead();
        }
        OnLifeChange(_currentLife / _startLife);
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
    void Dead()
    {
        enabled = false;
        OnDead();
    }
}
