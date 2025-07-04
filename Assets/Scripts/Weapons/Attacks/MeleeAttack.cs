using UnityEngine;

public class MeleeAttack : IAttackBehavior
{
    public string AttackName => "Melee Attack";
    
    private AttackEffectController _effectController;

    public void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        if (damageCollider != null)
        {
            damageCollider.SetActive(true);
            PlayAttackEffects(weaponData, attacker);
        }
        else
        {
            Debug.LogError("MeleeAttack: DamageCollider es NULL! No se puede activar el collider de daño.");
        }
    }

    public void StopAttack(GameObject damageCollider)
    {
        if (damageCollider != null)
        {
            damageCollider.SetActive(false);
        }
        else
        {
            Debug.LogError("MeleeAttack: DamageCollider es NULL! No se puede desactivar el collider de daño.");
        }
    }



    private void PlayAttackEffects(WeaponItem weaponData, Transform attacker)
    {
        if (_effectController == null)
        {
            MonoBehaviour coroutineRunner = attacker.GetComponent<MonoBehaviour>();
            if (coroutineRunner != null)
            {
                _effectController = new AttackEffectController(coroutineRunner);
            }
            else
            {
                Debug.LogError("MeleeAttack: No se pudo obtener MonoBehaviour del atacante!");
                return;
            }
        }

        if (weaponData.attackEffects != null && weaponData.attackEffects.Count > 0)
        {
            Transform weaponTransform = FindWeaponTransform(attacker, weaponData);
            _effectController.Initialize(weaponData.attackEffects, weaponTransform, attacker);
           
            if (!string.IsNullOrEmpty(weaponData.specificEffectID))
            {
                _effectController.ExecuteEffectByID(weaponData.specificEffectID, weaponData);
            }
            else
            {
                _effectController.ExecuteEffect(weaponData);
            }
        }
        else
        {
            Debug.LogWarning($"MeleeAttack: No hay efectos configurados para {weaponData.weaponName}. attackEffects: {(weaponData.attackEffects != null ? $"Count={weaponData.attackEffects.Count}" : "NULL")}");
        }
    }

    private Transform FindWeaponTransform(Transform attacker, WeaponItem weaponData)
    {
        Transform weaponTransform = null;
        
        if (!string.IsNullOrEmpty(weaponData.weaponTransformName))
        {
            weaponTransform = attacker.Find(weaponData.weaponTransformName);
            if (weaponTransform != null)
            {
                return weaponTransform;
            }
            else
            {
                Debug.LogWarning($"MeleeAttack: No se encontró el transform '{weaponData.weaponTransformName}', usando búsqueda automática");
            }
        }
        
        weaponTransform = attacker.Find("WeaponHolder");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("RightHand");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("Hand_R");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("mixamorig:RightHand");
            
        if (weaponTransform == null)
        {
            weaponTransform = attacker;
        }
        
        return weaponTransform;
    }
} 