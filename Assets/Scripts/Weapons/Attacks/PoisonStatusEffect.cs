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

    private void Start()
    {
        targetEntity = GetComponent<IEntity>();
    }

    public void ApplyPoison(float tickDamage, float duration, float tickInterval)
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }

        poisonTickDamage = tickDamage;
        currentPoisonDuration = duration;
        poisonTickInterval = tickInterval;
        isPoisoned = true;

        poisonCoroutine = StartCoroutine(PoisonEffect());
    }

    private IEnumerator PoisonEffect()
    {
        float elapsedTime = 0f;
        float nextTickTime = 0f;

        while (elapsedTime < currentPoisonDuration && targetEntity != null)
        {
            if (elapsedTime >= nextTickTime)
            {
                targetEntity.GetDamage(poisonTickDamage);
                nextTickTime += poisonTickInterval;
                
                ShowPoisonVisualEffect();
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isPoisoned = false;
        poisonCoroutine = null;
    }

    private void ShowPoisonVisualEffect()
    {
        Debug.Log($"POISON TICK: {poisonTickDamage} damage to {gameObject.name}");
    }

    public bool IsPoisoned()
    {
        return isPoisoned;
    }

    public float GetRemainingPoisonTime()
    {
        return isPoisoned ? currentPoisonDuration : 0f;
    }

    private void OnDestroy()
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }
    }
} 