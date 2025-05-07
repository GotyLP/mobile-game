using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupBase
{
    [SerializeField] private WeaponItem weaponData;

    public override void OnPickup(Player player)
    {
        player.Inventory.AddWeapon(new Weapon(weaponData, player.GetComponent<Attack>()));
    }
}
