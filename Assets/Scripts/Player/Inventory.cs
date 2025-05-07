using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private IWeapon _currentWeapon;

    public void AddWeapon(IWeapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void UseWeapon(Player player)
    {
        _currentWeapon?.Attack(player);
    }
}
