using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour, IEntity
{
    public PauseMenu pauseMenu;
    [SerializeField] float initialLife = 100;
    [SerializeField] float _currentLife;
    private void Awake()
    {
        _currentLife = initialLife;
    }
    public void GetDamage(float dmg)
    {
        _currentLife -= dmg;
        Debug.Log("Me daño");        
        EventManager.Trigger(new PlayerHealthChangedEvent(_currentLife, initialLife));
        if (_currentLife <= 0) 
        {
            OnDead();            
        }
    }
    public void OnDead()
    {
        EventManager.Trigger(SimpleEventType.PlayerDeathEvent);
        Time.timeScale = 0;
        Debug.Log("Dead");
    }
}

