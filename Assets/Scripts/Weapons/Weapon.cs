using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    private WeaponItem _weaponData;
    private float _lastAttackTime = -Mathf.Infinity;
    private Attack _attackComponent;

    public void Initialize(WeaponItem weaponData, Attack attackComponent)
    {
        _weaponData = weaponData;
        _attackComponent = attackComponent;
    }

    public void Attack(Player player)
    {
        if (Time.time - _lastAttackTime < _weaponData.attackCooldown)
            return;

        _lastAttackTime = Time.time;
        _attackComponent.ActivateObject();
        Debug.Log("¡Ataque de " + _weaponData.weaponName + "! Daño: " + _weaponData.damage);
    }
}
