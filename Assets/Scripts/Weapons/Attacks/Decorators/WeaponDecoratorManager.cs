using UnityEngine;
using System.Collections.Generic;

public class WeaponDecoratorManager
{
    private MeleeAttackBase _currentWeapon;
    private MeleeAttackBase _originalWeapon;
    private List<System.Type> _appliedDecorators;

    public WeaponDecoratorManager(MeleeAttackBase baseWeapon)
    {
        _originalWeapon = baseWeapon ?? throw new System.ArgumentNullException(nameof(baseWeapon));
        _currentWeapon = baseWeapon;
        _appliedDecorators = new List<System.Type>();
    }

    public MeleeAttackBase GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    public MeleeAttackBase GetOriginalWeapon()
    {
        return _originalWeapon;
    }

    public bool ApplyPoisonDecorator(PoisonedAttack.PoisonConfig poisonConfig = default)
    {
        if (HasDecorator<PoisonedAttack>())
        {
            Debug.LogWarning("WeaponDecoratorManager: El arma ya tiene veneno aplicado.");
            return false;
        }

        _currentWeapon = new PoisonedAttack(_currentWeapon, poisonConfig);
        _appliedDecorators.Add(typeof(PoisonedAttack));
        
        Debug.Log($"WeaponDecoratorManager: Aplicado decorator de veneno. Arma actual: {_currentWeapon.AttackName}");
        return true;
    }

    public bool RemoveDecorator<T>() where T : MeleeAttackDecorator
    {
        if (!HasDecorator<T>())
        {
            Debug.LogWarning($"WeaponDecoratorManager: El arma no tiene el decorator {typeof(T).Name} aplicado.");
            return false;
        }

        _currentWeapon = RebuildWeaponWithoutDecorator<T>();
        _appliedDecorators.Remove(typeof(T));
        
        Debug.Log($"WeaponDecoratorManager: Removido decorator {typeof(T).Name}. Arma actual: {_currentWeapon.AttackName}");
        return true;
    }

    public void RestoreOriginalWeapon()
    {
        _currentWeapon = _originalWeapon;
        _appliedDecorators.Clear();
        
        Debug.Log($"WeaponDecoratorManager: Restaurada arma original: {_currentWeapon.AttackName}");
    }

    public bool HasDecorator<T>() where T : MeleeAttackDecorator
    {
        return _appliedDecorators.Contains(typeof(T));
    }

    public List<System.Type> GetAppliedDecorators()
    {
        return new List<System.Type>(_appliedDecorators);
    }

    public bool HasAnyDecorator()
    {
        return _appliedDecorators.Count > 0;
    }

    private MeleeAttackBase RebuildWeaponWithoutDecorator<T>() where T : MeleeAttackDecorator
    {
        MeleeAttackBase rebuiltWeapon = _originalWeapon;

        foreach (var decoratorType in _appliedDecorators)
        {
            if (decoratorType != typeof(T))
            {
                rebuiltWeapon = ApplyDecoratorByType(rebuiltWeapon, decoratorType);
            }
        }

        return rebuiltWeapon;
    }

    private MeleeAttackBase ApplyDecoratorByType(MeleeAttackBase weapon, System.Type decoratorType)
    {
        if (decoratorType == typeof(PoisonedAttack))
        {
            return new PoisonedAttack(weapon);
        }

        Debug.LogError($"WeaponDecoratorManager: Tipo de decorator no soportado: {decoratorType.Name}");
        return weapon;
    }

    public WeaponDecoratorManager Clone()
    {
        var clonedManager = new WeaponDecoratorManager(_originalWeapon);
        
        foreach (var decoratorType in _appliedDecorators)
        {
            clonedManager._currentWeapon = clonedManager.ApplyDecoratorByType(clonedManager._currentWeapon, decoratorType);
            clonedManager._appliedDecorators.Add(decoratorType);
        }

        return clonedManager;
    }
} 