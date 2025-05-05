using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(LifeHandler))]
[RequireComponent(typeof(MinionSpawner))]
//[RequireComponent(typeof(MovementHandler))]

public class InputListener : MonoBehaviour
{
    LifeHandler _lifeHandler;
    MinionSpawner _minionSpawner;
    //MovementHandler _movementHandler;    

    Vector3 _movementDir;

    void Awake()
    {
        _minionSpawner = GetComponent<MinionSpawner>();
        _lifeHandler = GetComponent<LifeHandler>();
        //_movementHandler = GetComponent<MovementHandler>();

        _lifeHandler.onDead += ObjectIsDead;


        _movementDir = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _lifeHandler.GetDamage(25f);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            _minionSpawner.Spawn();
        }
    }
    private void FixedUpdate()
    {
        _movementDir.x = Input.GetAxis("Horizontal");
        _movementDir.z = Input.GetAxis("Vertical");
    }
    void ObjectIsDead()
    {
        this.enabled = false;
        _lifeHandler.onDead -= ObjectIsDead;
    }
}
