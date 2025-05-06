using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyFactory : MonoBehaviour
{
    public static BulletEnemyFactory Instance { get; private set; }
    
    [SerializeField] private BulletEnemy _prefab;
    
    [SerializeField] private int _initialAmount;
    
    private ObjectPool<BulletEnemy> _pool;

    private void Awake()
    {
        Instance = this;
        
        _pool = new ObjectPool<BulletEnemy>(CreateBullet, BulletEnemy.TurnOn, BulletEnemy.TurnOff, _initialAmount);
    }

    public BulletEnemy GetFromPool()
    {
        return _pool.GetObject();
    }

    public void ReturnToPool(BulletEnemy bullet)
    {
        _pool.ReturnToPool(bullet);
    }

    BulletEnemy CreateBullet()
    {
        return Instantiate(_prefab);
    }
}