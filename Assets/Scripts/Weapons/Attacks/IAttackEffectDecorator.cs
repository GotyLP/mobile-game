using UnityEngine;

public interface IAttackEffectDecorator
{
    SlashParameters ModifyParameters(SlashParameters originalParameters, WeaponItem weaponData);
    WeaponItem ModifyWeaponStats(WeaponItem originalWeapon);
    void OnPreEffect(Transform attackTransform, WeaponItem weaponData);
    void OnPostEffect(Transform attackTransform, WeaponItem weaponData);
    void ApplyStatusEffect(IEntity target, WeaponItem weaponData);
    int Priority { get; }
    string DecoratorName { get; }
} 