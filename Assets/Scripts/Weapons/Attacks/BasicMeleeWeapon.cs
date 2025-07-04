using UnityEngine;

public class BasicMeleeWeapon : MeleeAttackBase
{
    public override string AttackName => "Basic Melee Attack";

    public override void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        if (damageCollider != null)
        {
            damageCollider.SetActive(true);
            PlayAttackEffects(weaponData, attacker);
        }
        else
        {
            Debug.LogError("BasicMeleeWeapon: DamageCollider es NULL! No se puede activar el collider de daño.");
        }
    }

    public override void StopAttack(GameObject damageCollider)
    {
        if (damageCollider != null)
        {
            damageCollider.SetActive(false);
        }
        else
        {
            Debug.LogError("BasicMeleeWeapon: DamageCollider es NULL! No se puede desactivar el collider de daño.");
        }
    }

    public override void ApplyDamage(GameObject target, float damage, Transform attacker, WeaponItem weaponData)
    {
        base.ApplyDamage(target, damage, attacker, weaponData);
    }
} 