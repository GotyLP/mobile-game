using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackEffectController
{
    public List<SlashParameters> slashEffects;
    public Transform weaponTransform;
    public Transform attackerTransform;
    public bool cycleAttacks = true;
    
    private List<IAttackEffectDecorator> _decorators = new List<IAttackEffectDecorator>();
    private SlashParameters _currentSlash;
    private int _currentAttackIndex = 0;
    private MonoBehaviour _coroutineRunner;

    public AttackEffectController(MonoBehaviour coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        slashEffects = new List<SlashParameters>();
        
        if (slashEffects.Count == 0)
        {
            slashEffects.Add(new SlashParameters());
        }
        
        SelectEffect(0);
    }

    public void Initialize(List<SlashParameters> effects, Transform weapon, Transform attacker)
    {
        slashEffects = effects ?? new List<SlashParameters>();
        weaponTransform = weapon;
        attackerTransform = attacker;
        
        if (slashEffects.Count == 0)
        {
            Debug.LogWarning("AttackEffectController: No hay efectos asignados");
        }
        
        SelectEffect(0);
    }

    public void ExecuteEffect(WeaponItem weaponData)
    {
        if (_currentSlash == null)
        {
            Debug.LogWarning("AttackEffectController: No hay efecto seleccionado");
            return;
        }

        if (_coroutineRunner != null)
        {
            _coroutineRunner.StartCoroutine(ExecuteSlashCoroutine(_currentSlash, weaponData));
        }
        else
        {
            Debug.LogError("AttackEffectController: CoroutineRunner es NULL!");
        }
        
        if (cycleAttacks)
        {
            CycleToNextAttack();
        }
    }

    public void ExecuteEffectByID(string effectID, WeaponItem weaponData)
    {
        var effect = slashEffects.FirstOrDefault(e => e.effectID == effectID);
        if (effect != null)
        {
            if (_coroutineRunner != null)
            {
                _coroutineRunner.StartCoroutine(ExecuteSlashCoroutine(effect, weaponData));
            }
        }
        else
        {
            Debug.LogWarning($"AttackEffectController: No se encontró efecto con ID: {effectID}");
        }
    }

    private IEnumerator ExecuteSlashCoroutine(SlashParameters slashParams, WeaponItem weaponData)
    {
        SlashParameters modifiedParams = ApplyDecorators(slashParams, weaponData);
        
        ExecutePreEffects(weaponData);
        
        yield return new WaitForSeconds(modifiedParams.delay);
        
        if (weaponTransform == null)
        {
            Debug.LogError("AttackEffectController: ¡WeaponTransform es NULL!");
            yield break;
        }
        
        if (modifiedParams.slashEffect == null)
        {
            Debug.LogError("AttackEffectController: ¡SlashEffect prefab es NULL!");
            yield break;
        }

        Vector3 spawnPosition = weaponTransform.position + weaponTransform.TransformDirection(modifiedParams.positionOffset);
        Quaternion spawnRotation = weaponTransform.rotation * Quaternion.Euler(modifiedParams.rotationOffset);
        
        GameObject vfx = Object.Instantiate(modifiedParams.slashEffect, spawnPosition, spawnRotation);
        
        if (vfx != null)
        {
            VerifyAndFixParticleSystem(vfx);
            
            vfx.transform.localScale = modifiedParams.effectScale;
            
            float followTime = modifiedParams.followDuration > 0 ? modifiedParams.followDuration : modifiedParams.effectDuration;
            
            if (modifiedParams.followPosition || modifiedParams.followRotation)
            {
                _coroutineRunner.StartCoroutine(FollowWeapon(vfx, modifiedParams, followTime));
            }
            
            Object.Destroy(vfx, modifiedParams.effectDuration);
        }
        else
        {
            Debug.LogError("AttackEffectController: ¡Fallo al instanciar el efecto!");
        }
        
        ExecutePostEffects(weaponData);
    }

    private IEnumerator FollowWeapon(GameObject vfx, SlashParameters slashParams, float duration)
    {
        float elapsedTime = 0f;
        
        while (vfx != null && elapsedTime < duration && weaponTransform != null)
        {
            if (slashParams.followPosition)
            {
                vfx.transform.position = weaponTransform.position + weaponTransform.TransformDirection(slashParams.positionOffset);
            }
            
            if (slashParams.followRotation)
            {
                vfx.transform.rotation = weaponTransform.rotation * Quaternion.Euler(slashParams.rotationOffset);
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private SlashParameters ApplyDecorators(SlashParameters originalParams, WeaponItem weaponData)
    {
        SlashParameters modifiedParams = originalParams;
        
        var sortedDecorators = _decorators.OrderByDescending(d => d.Priority).ToList();
        
        foreach (var decorator in sortedDecorators)
        {
            modifiedParams = decorator.ModifyParameters(modifiedParams, weaponData);
        }
        
        return modifiedParams;
    }

    public WeaponItem GetModifiedWeaponStats(WeaponItem originalWeapon)
    {
        WeaponItem modifiedWeapon = originalWeapon;
        
        var sortedDecorators = _decorators.OrderByDescending(d => d.Priority).ToList();
        
        foreach (var decorator in sortedDecorators)
        {
            modifiedWeapon = decorator.ModifyWeaponStats(modifiedWeapon);
        }
        
        return modifiedWeapon;
    }

    public void ApplyStatusEffectsToTarget(IEntity target, WeaponItem weaponData)
    {
        foreach (var decorator in _decorators)
        {
            decorator.ApplyStatusEffect(target, weaponData);
        }
    }

    private void ExecutePreEffects(WeaponItem weaponData)
    {
        foreach (var decorator in _decorators)
        {
            decorator.OnPreEffect(attackerTransform, weaponData);
        }
    }

    private void ExecutePostEffects(WeaponItem weaponData)
    {
        foreach (var decorator in _decorators)
        {
            decorator.OnPostEffect(attackerTransform, weaponData);
        }
    }

    public void SelectEffect(int index)
    {
        if (slashEffects.Count == 0) return;
        
        int clampedIndex = Mathf.Clamp(index, 0, slashEffects.Count - 1);
        _currentAttackIndex = clampedIndex;
        _currentSlash = slashEffects[clampedIndex];
    }

    private void CycleToNextAttack()
    {
        int nextAttack = (_currentAttackIndex + 1) % slashEffects.Count;
        SelectEffect(nextAttack);
    }


    public void RegisterDecorator(IAttackEffectDecorator decorator)
    {
        if (!_decorators.Contains(decorator))
        {
            _decorators.Add(decorator);
        }
    }

    public void UnregisterDecorator(IAttackEffectDecorator decorator)
    {
        _decorators.Remove(decorator);
    }


    public List<IAttackEffectDecorator> GetDecorators()
    {
        return new List<IAttackEffectDecorator>(_decorators);
    }


    public void ClearDecorators()
    {
        _decorators.Clear();
    }

    private void VerifyAndFixParticleSystem(GameObject vfx)
    {
    }
} 