using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupBase
{
    [SerializeField] private WeaponItem weaponData;

    public override void OnPickup(Player player)
    {
        Weapon weapon = player.gameObject.AddComponent<Weapon>();
        weapon.Initialize(weaponData, player.AttackSystem);
        player.Inventory.AddWeapon(weaponData, weapon);
    }
}
