using System.Collections.Generic;
using UnityEngine;

public class AttackSystem
{
    private Dictionary<AttackType, IAttackBehavior> _attackBehaviors;
    private GameObject _damageCollider;
    private Transform _attacker;

    public AttackSystem(GameObject damageCollider, Transform attacker)
    {
        _damageCollider = damageCollider;
        _attacker = attacker;
        InitializeAttackBehaviors();
    }

    private void InitializeAttackBehaviors()
    {
        _attackBehaviors = new Dictionary<AttackType, IAttackBehavior>
        {
            { AttackType.Melee, new MeleeAttack() },
            { AttackType.Ranged, new RangedAttack() }
            // Agregar más tipos según necesidad
        };
    }

    public void ExecuteAttack(WeaponItem weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogWarning("No hay datos de arma para ejecutar el ataque");
            return;
        }

        if (_attackBehaviors.TryGetValue(weaponData.attackType, out IAttackBehavior attackBehavior))
        {
            attackBehavior.ExecuteAttack(weaponData, _damageCollider, _attacker);
        }
        else
        {
            Debug.LogError($"No se encontró comportamiento de ataque para el tipo: {weaponData.attackType}");
        }
    }

    public void StopAttack(WeaponItem weaponData)
    {
        if (weaponData == null) return;

        if (_attackBehaviors.TryGetValue(weaponData.attackType, out IAttackBehavior attackBehavior))
        {
            attackBehavior.StopAttack(_damageCollider);
        }
    }

    // Método para agregar nuevos tipos de ataque dinámicamente
    public void RegisterAttackBehavior(AttackType attackType, IAttackBehavior attackBehavior)
    {
        _attackBehaviors[attackType] = attackBehavior;
    }
} 