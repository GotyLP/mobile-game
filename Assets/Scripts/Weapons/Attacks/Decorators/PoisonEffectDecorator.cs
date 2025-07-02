using UnityEngine;
using System.Collections;

public class PoisonEffectDecorator : IAttackEffectDecorator
{
    public int Priority => 10;
    public string DecoratorName => "Poison Effect";
    
    public GameObject poisonParticlesPrefab;
    public float poisonDamageMultiplier = 1.3f;
    public float poisonCooldownReduction = 0.8f;
    public Vector3 poisonEffectScale = new Vector3(1.1f, 1.1f, 1.1f);
    public float poisonDuration = 5f;
    public float poisonTickDamage = 2f;
    public float poisonTickInterval = 1f;

    public PoisonEffectDecorator(GameObject poisonParticles = null)
    {
        poisonParticlesPrefab = poisonParticles;
    }

    public SlashParameters ModifyParameters(SlashParameters originalParameters, WeaponItem weaponData)
    {
        SlashParameters modifiedParams = CreateCopy(originalParameters);
        
        modifiedParams.effectScale = Vector3.Scale(modifiedParams.effectScale, poisonEffectScale);
        modifiedParams.effectDuration = Mathf.Max(modifiedParams.effectDuration, poisonDuration);
        modifiedParams.effectID = modifiedParams.effectID + "_poison";
        modifiedParams.rotationOffset += new Vector3(0, 15f, 0);
        
        return modifiedParams;
    }

    public WeaponItem ModifyWeaponStats(WeaponItem originalWeapon)
    {
        WeaponItem modifiedWeapon = Object.Instantiate(originalWeapon);
        
        modifiedWeapon.damage *= poisonDamageMultiplier;
        modifiedWeapon.attackCooldown *= poisonCooldownReduction;
        modifiedWeapon.weaponName = originalWeapon.weaponName + " (Envenenada)";
        
        return modifiedWeapon;
    }

    public void OnPreEffect(Transform attackTransform, WeaponItem weaponData)
    {
        if (poisonParticlesPrefab != null && attackTransform != null)
        {
            GameObject prePoisonEffect = Object.Instantiate(poisonParticlesPrefab, 
                attackTransform.position, attackTransform.rotation);
            Object.Destroy(prePoisonEffect, 0.8f);
        }
    }

    public void OnPostEffect(Transform attackTransform, WeaponItem weaponData)
    {
        if (poisonParticlesPrefab != null && attackTransform != null)
        {
            GameObject postPoisonEffect = Object.Instantiate(poisonParticlesPrefab, 
                attackTransform.position + Vector3.up * 0.3f, 
                attackTransform.rotation);
            Object.Destroy(postPoisonEffect, 1.5f);
        }
    }

    public void ApplyStatusEffect(IEntity target, WeaponItem weaponData)
    {
        if (target != null)
        {
            PoisonStatusEffect poisonEffect = target as PoisonStatusEffect;
            if (poisonEffect != null)
            {
                poisonEffect.ApplyPoison(poisonTickDamage, poisonDuration, poisonTickInterval);
            }
            else
            {
                GameObject targetGameObject = (target as MonoBehaviour)?.gameObject;
                if (targetGameObject != null)
                {
                    PoisonStatusEffect newPoisonEffect = targetGameObject.GetComponent<PoisonStatusEffect>();
                    if (newPoisonEffect == null)
                    {
                        newPoisonEffect = targetGameObject.AddComponent<PoisonStatusEffect>();
                    }
                    newPoisonEffect.ApplyPoison(poisonTickDamage, poisonDuration, poisonTickInterval);
                }
            }
        }
    }

    private SlashParameters CreateCopy(SlashParameters original)
    {
        SlashParameters copy = new SlashParameters
        {
            slashEffect = original.slashEffect,
            delay = original.delay,
            followPosition = original.followPosition,
            followRotation = original.followRotation,
            followDuration = original.followDuration,
            positionOffset = original.positionOffset,
            rotationOffset = original.rotationOffset,
            effectDuration = original.effectDuration,
            effectScale = original.effectScale,
            effectID = original.effectID
        };
        return copy;
    }
} 