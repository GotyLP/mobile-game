using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bridge/Adapter between Unity's MonoBehaviour world and MVC Model
/// Receives damage from scene interactions and delegates to PlayerModel
/// </summary>
public class LifeHandler : MonoBehaviour, IEntity
{
    private Player _player;
    private PlayerModel _model;


    void Start()
    {
        _player = GetComponent<Player>();        
        if (_player != null)
        {
            _model = _player.Model;
        }
        else
        {
            Debug.LogError("LifeHandler: Cannot find Player component");
        }
        EventManager.Subscribe(SimpleEventType.PlayerDeathEvent, OnDead);
    }

    void OnDestroy()
    {
        EventManager.Unsubscribe(SimpleEventType.PlayerDeathEvent, OnDead);
    }

    /// <summary>
    /// Called by external systems (bullets, enemies, etc.) via IEntity interface
    /// </summary>
    public void GetDamage(float dmg)
    {
        if (_model != null)
        {
            Debug.Log($"LifeHandler: Received {dmg} damage, delegating to Model");
            _model.TakeDamage(dmg);
        }
    }

    public void OnDead()
    {
        Time.timeScale = 0; // Pause the game
        Debug.Log("Player Dead - Game Paused");        
    }
}


