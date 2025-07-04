using UnityEngine;

[System.Serializable]
public class WeaponUpgradeShopItem
{
    [Header("Información del Item")]
    public string itemID;
    public string itemName;
    public string description;
    public Sprite icon;
    public int price;

    [Header("Configuración del Upgrade")]
    public UpgradeType upgradeType;
    public WeaponItem associatedWeapon;

    [Header("Configuración de Veneno")]
    public PoisonedAttack.PoisonConfig poisonConfig = PoisonedAttack.PoisonConfig.Default;

    public enum UpgradeType
    {
        PoisonEffect
    }

    public bool ApplyUpgrade(AttackSystem attackSystem)
    {
        if (attackSystem == null)
        {
            Debug.LogError($"WeaponUpgradeShopItem: AttackSystem es null, no se puede aplicar {itemName}");
            return false;
        }

        switch (upgradeType)
        {
            case UpgradeType.PoisonEffect:
                return ApplyPoisonUpgrade(attackSystem);

            default:
                Debug.LogError($"WeaponUpgradeShopItem: Tipo de mejora desconocido: {upgradeType}");
                return false;
        }
    }

    public bool IsUpgradeApplied(AttackSystem attackSystem)
    {
        if (attackSystem == null) return false;

        switch (upgradeType)
        {
            case UpgradeType.PoisonEffect:
                return attackSystem.MeleeWeaponHasDecorator<PoisonedAttack>();

            default:
                return false;
        }
    }

    public bool RemoveUpgrade(AttackSystem attackSystem)
    {
        if (attackSystem == null) return false;

        switch (upgradeType)
        {
            case UpgradeType.PoisonEffect:
                return attackSystem.RemovePoisonFromMeleeWeapon();

            default:
                Debug.LogWarning($"WeaponUpgradeShopItem: Remoción no implementada para {upgradeType}");
                return false;
        }
    }

    private bool ApplyPoisonUpgrade(AttackSystem attackSystem)
    {
        if (attackSystem.MeleeWeaponHasDecorator<PoisonedAttack>())
        {
            Debug.LogWarning($"WeaponUpgradeShopItem: El arma ya tiene el efecto de veneno aplicado");
            return false;
        }

        bool success = attackSystem.ApplyPoisonToMeleeWeapon(poisonConfig);
        
        if (success)
        {
            Debug.Log($"WeaponUpgradeShopItem: Aplicado {itemName} exitosamente - Veneno: {poisonConfig.tickDamage} daño cada {poisonConfig.tickInterval}s por {poisonConfig.duration}s");
        }

        return success;
    }

    public string GetDetailedDescription()
    {
        string baseDescription = $"{description}\n\nPrecio: {price} monedas";

        switch (upgradeType)
        {
            case UpgradeType.PoisonEffect:
                baseDescription += $"\n\nEfecto de Veneno:";
                baseDescription += $"\n• Daño por tick: {poisonConfig.tickDamage}";
                baseDescription += $"\n• Duración: {poisonConfig.duration}s";
                baseDescription += $"\n• Intervalo: {poisonConfig.tickInterval}s";
                break;
        }

        if (associatedWeapon != null)
        {
            baseDescription += $"\n\nArma asociada: {associatedWeapon.weaponName}";
        }

        return baseDescription;
    }

    public bool CanPurchase(int playerMoney, AttackSystem attackSystem)
    {
        if (playerMoney < price)
        {
            return false;
        }

        if (IsUpgradeApplied(attackSystem))
        {
            return false;
        }

        return true;
    }
} 