using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float _damageValue = 10f;
    private void OnTriggerEnter(Collider other)
    {
        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            Debug.Log("DAMAGE ENEMY");
            entity.GetDamage (_damageValue);
        }
        
    }
}
