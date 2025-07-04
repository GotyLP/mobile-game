using System.Collections.Generic;
using UnityEngine;

public class AttackSystem
{
    private Dictionary<AttackType, IAttackBehavior> _attackBehaviors;
    private Dictionary<AttackType, WeaponDecoratorManager> _weaponManagers;
    private GameObject _damageCollider;
    private Transform _attacker;

    public AttackSystem(GameObject damageCollider, Transform attacker)
    {
        _damageCollider = damageCollider;
        _attacker = attacker;
        
        InitializeAttackBehaviors();
        InitializeWeaponManagers();
    }

    private void InitializeAttackBehaviors()
    {
        _attackBehaviors = new Dictionary<AttackType, IAttackBehavior>
        {
            { AttackType.Melee, new BasicMeleeWeapon() },
            { AttackType.Ranged, new RangedAttack() }
        };
    }

    private void InitializeWeaponManagers()
    {
        _weaponManagers = new Dictionary<AttackType, WeaponDecoratorManager>
        {
            { AttackType.Melee, new WeaponDecoratorManager(new BasicMeleeWeapon()) }
        };
    }

    public void ExecuteAttack(WeaponItem weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogWarning("AttackSystem: No hay datos de arma para ejecutar el ataque");
            return;
        }

        if (weaponData.attackType == AttackType.Melee && _weaponManagers.TryGetValue(weaponData.attackType, out WeaponDecoratorManager weaponManager))
        {
            var currentWeapon = weaponManager.GetCurrentWeapon();
            currentWeapon.ExecuteAttack(weaponData, _damageCollider, _attacker);
        }
        else if (_attackBehaviors.TryGetValue(weaponData.attackType, out IAttackBehavior attackBehavior))
        {
            attackBehavior.ExecuteAttack(weaponData, _damageCollider, _attacker);
        }
        else
        {
            Debug.LogError($"AttackSystem: No se encontró comportamiento de ataque para el tipo: {weaponData.attackType}");
        }
    }

    public void StopAttack(WeaponItem weaponData)
    {
        if (weaponData == null) 
        {
            Debug.LogWarning("AttackSystem: No hay datos de arma para detener el ataque");
            return;
        }

        if (weaponData.attackType == AttackType.Melee && _weaponManagers.TryGetValue(weaponData.attackType, out WeaponDecoratorManager weaponManager))
        {
            var currentWeapon = weaponManager.GetCurrentWeapon();
            currentWeapon.StopAttack(_damageCollider);
        }
        else if (_attackBehaviors.TryGetValue(weaponData.attackType, out IAttackBehavior attackBehavior))
        {
            attackBehavior.StopAttack(_damageCollider);
        }
    }

    public void RegisterAttackBehavior(AttackType attackType, IAttackBehavior attackBehavior)
    {
        _attackBehaviors[attackType] = attackBehavior;
    }

    public bool ApplyPoisonToMeleeWeapon(PoisonedAttack.PoisonConfig poisonConfig = default)
    {
        if (_weaponManagers.TryGetValue(AttackType.Melee, out WeaponDecoratorManager weaponManager))
        {
            return weaponManager.ApplyPoisonDecorator(poisonConfig);
        }
        
        Debug.LogError("AttackSystem: No se encontró el gestor de armas melee");
        return false;
    }

    public bool RemovePoisonFromMeleeWeapon()
    {
        if (_weaponManagers.TryGetValue(AttackType.Melee, out WeaponDecoratorManager weaponManager))
        {
            return weaponManager.RemoveDecorator<PoisonedAttack>();
        }
        
        Debug.LogError("AttackSystem: No se encontró el gestor de armas melee");
        return false;
    }

    public void RestoreMeleeWeaponToOriginal()
    {
        if (_weaponManagers.TryGetValue(AttackType.Melee, out WeaponDecoratorManager weaponManager))
        {
            weaponManager.RestoreOriginalWeapon();
        }
        else
        {
            Debug.LogError("AttackSystem: No se encontró el gestor de armas melee");
        }
    }

    public bool MeleeWeaponHasDecorator<T>() where T : MeleeAttackDecorator
    {
        if (_weaponManagers.TryGetValue(AttackType.Melee, out WeaponDecoratorManager weaponManager))
        {
            return weaponManager.HasDecorator<T>();
        }
        
        return false;
    }

    public WeaponDecoratorManager GetWeaponDecoratorManager(AttackType attackType)
    {
        _weaponManagers.TryGetValue(attackType, out WeaponDecoratorManager weaponManager);
        return weaponManager;
    }

    public string GetMeleeWeaponStatus()
    {
        if (_weaponManagers.TryGetValue(AttackType.Melee, out WeaponDecoratorManager weaponManager))
        {
            var currentWeapon = weaponManager.GetCurrentWeapon();
            var appliedDecorators = weaponManager.GetAppliedDecorators();
            
            string status = $"Arma actual: {currentWeapon.AttackName}";
            if (appliedDecorators.Count > 0)
            {
                status += $"\nDecorators aplicados: {string.Join(", ", appliedDecorators.ConvertAll(t => t.Name))}";
            }
            
            return status;
        }
        
        return "No hay gestor de armas melee disponible";
    }
} 