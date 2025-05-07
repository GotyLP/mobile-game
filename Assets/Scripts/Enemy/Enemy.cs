using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEntity
{
    [SerializeField] private float _life;
    [SerializeField] private float _maxLife = 100f;

    private void Awake()
    {
        _life = _maxLife;
    }

    public void GetDamage(float damage)
    {
        Debug.Log("AUCH");
        _life -= damage;
        if (_life < 0)
        {
            OnDead();
        }
    }
    public void OnDead()
    {
        gameObject.SetActive(false);        
    }

}
