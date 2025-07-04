using UnityEngine;

public class PoisonedAttack : MeleeAttackDecorator
{
    public override string AttackName => $"Poisoned {_decoratedAttack.AttackName}";

    [System.Serializable]
    public struct PoisonConfig
    {
        public float tickDamage;
        public float duration;
        public float tickInterval;
        public GameObject poisonVisualEffectPrefab;
        
        public static PoisonConfig Default => new PoisonConfig
        {
            tickDamage = 2f,
            duration = 5f,
            tickInterval = 1f,
            poisonVisualEffectPrefab = null
        };
    }

    private PoisonConfig _poisonConfig;

    public PoisonedAttack(MeleeAttackBase decoratedAttack, PoisonConfig poisonConfig = default) 
        : base(decoratedAttack)
    {
        _poisonConfig = poisonConfig.Equals(default(PoisonConfig)) ? PoisonConfig.Default : poisonConfig;
    }

    public PoisonedAttack(MeleeAttackBase decoratedAttack, float poisonTickDamage, float poisonDuration, float poisonTickInterval) 
        : base(decoratedAttack)
    {
        _poisonConfig = new PoisonConfig
        {
            tickDamage = poisonTickDamage,
            duration = poisonDuration,
            tickInterval = poisonTickInterval,
            poisonVisualEffectPrefab = null
        };
    }

    public override void ApplyDamage(GameObject target, float damage, Transform attacker, WeaponItem weaponData)
    {
        base.ApplyDamage(target, damage, attacker, weaponData);
        ApplyPoisonEffect(target);
    }

    private void ApplyPoisonEffect(GameObject target)
    {
        if (target == null) return;

        PoisonStatusEffect poisonEffect = target.GetComponent<PoisonStatusEffect>();
        if (poisonEffect == null)
        {
            poisonEffect = target.AddComponent<PoisonStatusEffect>();
        }

        poisonEffect.ApplyPoison(_poisonConfig.tickDamage, _poisonConfig.duration, _poisonConfig.tickInterval, _poisonConfig.poisonVisualEffectPrefab);

        Debug.Log($"🟢 VENENO APLICADO a {target.name} - Tick: {_poisonConfig.tickDamage} daño cada {_poisonConfig.tickInterval}s por {_poisonConfig.duration}s");
    }

    public PoisonConfig GetPoisonConfig()
    {
        return _poisonConfig;
    }

    public void UpdatePoisonConfig(PoisonConfig newConfig)
    {
        _poisonConfig = newConfig;
    }
} 