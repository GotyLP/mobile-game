using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject DamageCollider;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    public void ActivateObject()
    {
        if (_inventory == null || _inventory.GetCurrentWeaponData() == null)
        {
            Debug.Log("No hay un arma equipada para atacar");
            return;
        }

        if (DamageCollider != null)
            DamageCollider.SetActive(true);
    }

    public void DeactivateObject()
    {
        if (DamageCollider != null)
            DamageCollider.SetActive(false);
    }
}
