using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LifeHandler : MonoBehaviour, IEntity
{
    private Player _player;
    private PlayerModel _model;


    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Contains("Menu") || currentSceneName.Contains("MainMenu"))
        {
            Debug.LogWarning($"LifeHandler: Detectado en escena de menú '{currentSceneName}'. Desactivando componente.");
            enabled = false;
            return;
        }

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


