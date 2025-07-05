using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEntity
{
    public VictoryByDestruction manager;// Referencia al script que maneja la victoria por destrucci�n
    public int puntosAlMorir = 10;
    private float _life;
    [SerializeField] private float _maxLife = 100f;
    private float _baseDamage = 25f;

    private void Start()
    {
        // Aplicar multiplicadores desde Remote Config
        float difficultyMultiplier = RemoteConfig.Instance != null ? RemoteConfig.Instance.GameDifficulty : 1f;
        _maxLife *= difficultyMultiplier;
        _baseDamage *= difficultyMultiplier;
        _life = _maxLife;
    }

    public void GetDamage(float damage)
    {
        _life -= damage;
        if (_life < 0)
        {
            OnDead();
        }
    }

    public void OnDead()
    {
        if (manager != null)
        {
            manager.RegistrarDestruccion();
            Debug.Log("Enemy destroyed, points awarded: " + puntosAlMorir);
        }
        if (CurrenciManager.Instance != null)
        {
            CurrenciManager.Instance.AddPoints(puntosAlMorir);
        }
        gameObject.SetActive(false);        
    }

    protected float GetBaseDamage()
    {
        return _baseDamage;
    }
}
