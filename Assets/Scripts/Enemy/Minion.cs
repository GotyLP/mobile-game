using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeHandler))]
public class Minion : MonoBehaviour
{
    //private LifeHandler _ownerLifeHandler;

    //private LifeHandler _myLifeHandler;

    //private void Awake()
    //{
    //    _myLifeHandler = GetComponent<LifeHandler>();
    //    _myLifeHandler.onDead += OwnerDead;
    //}
    //public void Initilize(MinionSpawner owner)
    //{
    //    if (owner. TryGetComponent(out _ownerLifeHandler))
    //    {
    //        _ownerLifeHandler.onDead += ownerDead;
    //    }
    //}
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        _myLifeHandler.GetDamage(Random.Range(75, 150))
    //    }
    //}
    //void onDead()
    //{
    //    _ownerLifeHandler.onDead -= OwnerDead;
    //    Destroy(gameObject);
    //}
}
