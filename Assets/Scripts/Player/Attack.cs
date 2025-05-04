using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject DamageCollider;
    public void ActivateObject()
    {
        if (DamageCollider != null)
            DamageCollider.SetActive(true);
    }

    public void DeactivateObject()
    {
        if (DamageCollider != null)
            DamageCollider.SetActive(false);
    }
}
