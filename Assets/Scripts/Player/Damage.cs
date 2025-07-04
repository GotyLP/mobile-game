using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Inventory _inventory;
    private Player _player;

    private void Awake()
    {
        _inventory = GetComponentInParent<Inventory>();
        _player = GetComponentInParent<Player>();
        
        if (_inventory == null)
        {
            Debug.LogError("No se encontró el componente Inventory en el padre del objeto Damage. Asegúrate de que el Damage esté en un hijo del Player.");
        }
        
        if (_player == null)
        {
            Debug.LogError("No se encontró el componente Player en el padre del objeto Damage.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_inventory == null || _player == null) return;

        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            WeaponItem currentWeaponData = _inventory.GetCurrentWeaponData();
            if (currentWeaponData != null)
            {
                
                if (currentWeaponData.attackType == AttackType.Melee)
                {
                    var attackSystem = _player.AttackSystem;
                    var weaponManager = attackSystem.GetWeaponDecoratorManager(AttackType.Melee);
                    
                    if (weaponManager != null)
                    {
                        var currentWeapon = weaponManager.GetCurrentWeapon();
                        currentWeapon.ApplyDamage(other.gameObject, currentWeaponData.damage, _player.transform, currentWeaponData);
                    }
                    else
                    {
                        entity.GetDamage(currentWeaponData.damage);
                    }
                }
                else
                {
                    entity.GetDamage(currentWeaponData.damage);
                }
            }
        }
    }
}
