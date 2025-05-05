using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : Factory
{
    [SerializeField] private  Enemy _enemyPrefab;
    public override IProduct GetProduct (Vector3 position)
    {
        IProduct obj = Instantiate (_enemyPrefab, position, Quaternion.identity);

        obj.Initialize();

        return obj;
    }
}
