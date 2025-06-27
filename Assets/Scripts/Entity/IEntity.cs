using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    void GetDamage(float damage);
    void OnDead();
}
