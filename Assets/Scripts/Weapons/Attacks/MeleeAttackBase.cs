using UnityEngine;

public abstract class MeleeAttackBase : IAttackBehavior
{
    public abstract string AttackName { get; }
    
    protected AttackEffectController _effectController;

    public abstract void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker);

    public abstract void StopAttack(GameObject damageCollider);

    public virtual void ApplyDamage(GameObject target, float damage, Transform attacker, WeaponItem weaponData)
    {
        if (target == null) return;

        var entity = target.GetComponent<IEntity>();
        if (entity != null)
        {
            Debug.Log($"MeleeAttackBase: Aplicando {damage} de daño a {target.name}");
            entity.GetDamage(damage);
        }
    }

    protected virtual void PlayAttackEffects(WeaponItem weaponData, Transform attacker)
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
                Debug.LogError("MeleeAttackBase: No se pudo obtener MonoBehaviour del atacante!");
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
    }

    protected virtual Transform FindWeaponTransform(Transform attacker, WeaponItem weaponData)
    {
        Transform weaponTransform = null;
        
        if (!string.IsNullOrEmpty(weaponData.weaponTransformName))
        {
            weaponTransform = attacker.Find(weaponData.weaponTransformName);
            if (weaponTransform != null)
            {
                return weaponTransform;
            }
        }
        
        weaponTransform = attacker.Find("WeaponHolder");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("RightHand");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("Hand_R");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("mixamorig:RightHand");
            
        return weaponTransform ?? attacker;
    }
} 