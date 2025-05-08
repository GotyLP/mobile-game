using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponentInParent<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("No se encontró el componente Inventory en el padre del objeto Damage. Asegúrate de que el Damage esté en un hijo del Player.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_inventory == null) return;

        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            WeaponItem currentWeaponData = _inventory.GetCurrentWeaponData();
            if (currentWeaponData != null)
            {
                entity.GetDamage(currentWeaponData.damage);
            }
            else
            {
                Debug.LogWarning("No hay un arma equipada actualmente");
            }
        }
    }
}
