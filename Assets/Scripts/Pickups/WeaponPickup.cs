using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupBase
{
    [SerializeField] private WeaponItem weaponData;

    public override void OnPickup(Player player)
    {
        player.Weapon.Initialize(weaponData, player.AttackSystem);
        player.Inventory.AddWeapon(weaponData, player.Weapon);
    }
}
