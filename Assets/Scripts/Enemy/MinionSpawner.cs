using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] Minion _minionPrefab;

    public void Spawn()
    {
        var newMinion = Instantiate(_minionPrefab, transform.position + transform.forward * 2+ transform.right * 2,Quaternion.identity);
        newMinion.Initilize(this);

        newMinion = Instantiate(_minionPrefab,transform.position + transform.forward * 2 + transform.right * 2, Quaternion.identity);   
        newMinion.Initilize(this);
    }
}
