using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : ScriptableObject
{
    public string weaponName = "Arma";
    public float damage = 10f;
    public float attackCooldown = 0.5f;
    public float energyCost = 10f;
}