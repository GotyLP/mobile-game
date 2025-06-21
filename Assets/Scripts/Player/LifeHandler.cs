using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bridge/Adapter between Unity's MonoBehaviour world and MVC Model
/// Receives damage from scene interactions and delegates to PlayerModel
/// </summary>
public class LifeHandler : MonoBehaviour, IEntity
{
    private PlayerMVC.Player player;
    private PlayerMVC.PlayerModel model;

    void Awake()
    {
        player = GetComponent<PlayerMVC.Player>();
        
        if (player != null)
        {
            model = player.Model;
        }
        else
        {
            Debug.LogError("LifeHandler: Cannot find PlayerMVC.Player component");
        }
    }

    void Start()
    {
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
        if (model != null)
        {
            Debug.Log($"LifeHandler: Received {dmg} damage, delegating to Model");
            model.TakeDamage(dmg);
        }
    }

    public void OnDead()
    {
        Time.timeScale = 0; // Pause the game
        Debug.Log("Player Dead - Game Paused");        
    }
}


