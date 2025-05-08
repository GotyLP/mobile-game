using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, IWeapon> _weapons = new Dictionary<string, IWeapon>();
    private IWeapon _currentWeapon;
    private WeaponItem _currentWeaponData;

    public void AddWeapon(WeaponItem weaponData, IWeapon weapon)
    {
        string key = weaponData.weaponName;
        if (!_weapons.ContainsKey(key))
            _weapons.Add(key, weapon);
        _currentWeapon = weapon;
        _currentWeaponData = weaponData;
    }

    public void UseWeapon(Player player)
    {
        _currentWeapon?.Attack(player);
    }

    public WeaponItem GetCurrentWeaponData()
    {
        return _currentWeaponData;
    }
}
