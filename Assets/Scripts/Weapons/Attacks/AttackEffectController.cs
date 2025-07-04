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
        yield return new WaitForSeconds(slashParams.delay);
        
        if (weaponTransform == null)
        {
            Debug.LogError("AttackEffectController: ¡WeaponTransform es NULL!");
            yield break;
        }
        
        if (slashParams.slashEffect == null)
        {
            Debug.LogError("AttackEffectController: ¡SlashEffect prefab es NULL!");
            yield break;
        }

        Vector3 spawnPosition = weaponTransform.position + weaponTransform.TransformDirection(slashParams.positionOffset);
        Quaternion spawnRotation = weaponTransform.rotation * Quaternion.Euler(slashParams.rotationOffset);
        
        GameObject vfx = Object.Instantiate(slashParams.slashEffect, spawnPosition, spawnRotation);
        
        if (vfx != null)
        {
            VerifyAndFixParticleSystem(vfx);
            
            vfx.transform.localScale = slashParams.effectScale;
            
            float followTime = slashParams.followDuration > 0 ? slashParams.followDuration : slashParams.effectDuration;
            
            if (slashParams.followPosition || slashParams.followRotation)
            {
                _coroutineRunner.StartCoroutine(FollowWeapon(vfx, slashParams, followTime));
            }
            
            Object.Destroy(vfx, slashParams.effectDuration);
        }
        else
        {
            Debug.LogError("AttackEffectController: ¡Fallo al instanciar el efecto!");
        }
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

    private void VerifyAndFixParticleSystem(GameObject vfx)
    {
    }
} 