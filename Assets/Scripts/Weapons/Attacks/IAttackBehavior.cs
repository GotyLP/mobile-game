using UnityEngine;

public interface IAttackBehavior
{
    void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker);
    void StopAttack(GameObject damageCollider);
    string AttackName { get; }
} 