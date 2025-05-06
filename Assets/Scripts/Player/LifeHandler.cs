using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour, IEntity
{
    [SerializeField] float initialLife = 100;
    [SerializeField] float _currentLife;

    //public event Action onDead = delegate { };

    private void Awake()
    {
        _currentLife = initialLife;
    }
    public void GetDamage(float dmg)
    {
        _currentLife -= dmg;
        Debug.Log("Me daño");
        if (_currentLife <= 0) 
        {
            //onDead();
            Debug.Log("Dead");
        }
    }
}

