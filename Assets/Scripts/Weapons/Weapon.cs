using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    private WeaponItem _weaponData;
    private float _lastAttackTime = -Mathf.Infinity;
    private AttackSystem _attackSystem;

    public void Initialize(WeaponItem weaponData, AttackSystem attackSystem)
    {
        _weaponData = weaponData;
        _attackSystem = attackSystem;
    }

    public void Attack(Player player)
    {
        if (Time.time - _lastAttackTime < _weaponData.attackCooldown)
            return;

        _lastAttackTime = Time.time;
        _attackSystem?.ExecuteAttack(_weaponData);
    }

    public void StopAttack()
    {
        _attackSystem?.StopAttack(_weaponData);
    }
}
