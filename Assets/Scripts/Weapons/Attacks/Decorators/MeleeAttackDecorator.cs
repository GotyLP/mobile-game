using UnityEngine;

public abstract class MeleeAttackDecorator : MeleeAttackBase
{
    protected MeleeAttackBase _decoratedAttack;
    protected MeleeAttackBase _previousWeapon;

    public MeleeAttackDecorator(MeleeAttackBase decoratedAttack)
    {
        _decoratedAttack = decoratedAttack ?? throw new System.ArgumentNullException(nameof(decoratedAttack));
        
        if (decoratedAttack is MeleeAttackDecorator decorator)
        {
            _previousWeapon = decorator.GetPreviousWeapon();
        }
        else
        {
            _previousWeapon = decoratedAttack;
        }
    }

    public virtual MeleeAttackBase GetPreviousWeapon()
    {
        return _previousWeapon;
    }

    public virtual MeleeAttackBase GetDecoratedAttack()
    {
        return _decoratedAttack;
    }

    public virtual MeleeAttackBase RestoreOriginalWeapon()
    {
        return _previousWeapon;
    }

    public override void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        _decoratedAttack.ExecuteAttack(weaponData, damageCollider, attacker);
    }

    public override void StopAttack(GameObject damageCollider)
    {
        _decoratedAttack.StopAttack(damageCollider);
    }

    public override void ApplyDamage(GameObject target, float damage, Transform attacker, WeaponItem weaponData)
    {
        _decoratedAttack.ApplyDamage(target, damage, attacker, weaponData);
    }
} 