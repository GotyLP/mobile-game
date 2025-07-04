using UnityEngine;
using System.Collections;

public class PoisonStatusEffect : MonoBehaviour
{
    private bool isPoisoned = false;
    private float currentPoisonDuration = 0f;
    private float poisonTickDamage = 0f;
    private float poisonTickInterval = 0f;
    private IEntity targetEntity;
    private Coroutine poisonCoroutine;
    private GameObject poisonVisualEffectPrefab;
    private GameObject activePoisonEffect;

    private void Start()
    {
        targetEntity = GetComponent<IEntity>();
    }

    public void ApplyPoison(float tickDamage, float duration, float tickInterval, GameObject poisonVisualEffectPrefab = null)
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }

        poisonTickDamage = tickDamage;
        currentPoisonDuration = duration;
        poisonTickInterval = tickInterval;
        isPoisoned = true;
        this.poisonVisualEffectPrefab = poisonVisualEffectPrefab;
        
        CreatePoisonVisualEffect();
        
        Debug.Log($"💀 VENENO INICIADO en {gameObject.name} - {tickDamage} daño cada {tickInterval}s por {duration}s");
        poisonCoroutine = StartCoroutine(PoisonEffect());
    }

    private IEnumerator PoisonEffect()
    {
        float elapsedTime = 0f;
        float nextTickTime = 0f;
        int tickCount = 0;

        while (elapsedTime < currentPoisonDuration && targetEntity != null && gameObject.activeInHierarchy)
        {
            if (elapsedTime >= nextTickTime)
            {
                if (targetEntity != null && gameObject.activeInHierarchy)
                {
                    tickCount++;
                    targetEntity.GetDamage(poisonTickDamage);
                    nextTickTime += poisonTickInterval;
                }
                else
                {
                    break;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CleanupPoisonEffect(tickCount);
    }

    private void CreatePoisonVisualEffect()
    {
        if (poisonVisualEffectPrefab != null && activePoisonEffect == null)
        {
            activePoisonEffect = Instantiate(poisonVisualEffectPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    public bool IsPoisoned()
    {
        return isPoisoned;
    }

    private void CleanupPoisonEffect(int tickCount)
    {
        isPoisoned = false;
        poisonVisualEffectPrefab = null;
        poisonCoroutine = null;
        
        CleanupVisualEffects();
    }

    private void CleanupVisualEffects()
    {
        if (activePoisonEffect != null)
        {
            ParticleSystem particle = activePoisonEffect.GetComponent<ParticleSystem>();
            if (particle != null)
            {
                particle.Stop();
            }
            Destroy(activePoisonEffect, 1f);
            activePoisonEffect = null;
        }
    }

    public void StopPoison()
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
            CleanupPoisonEffect(0);
        }
    }

    private void OnDestroy()
    {
        StopPoison();
    }

    private void OnDisable()
    {
        StopPoison();
    }
} 