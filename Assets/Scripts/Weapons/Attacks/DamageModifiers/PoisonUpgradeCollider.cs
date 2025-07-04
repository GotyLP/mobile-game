using UnityEngine;

public class PoisonUpgradeCollider : MonoBehaviour
{
    [Header("Configuración del Veneno")]
    public float poisonTickDamage = 3f;
    public float poisonDuration = 6f;
    public float poisonTickInterval = 1.5f;
    
    [Header("Settings")]
    public bool destroyAfterUse = true;
    public bool canApplyMultipleTimes = false;
    public GameObject poisonVisualEffectPrefab;
    private bool hasBeenUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenUsed && !canApplyMultipleTimes) return;
        
        if (other.CompareTag("Player"))
        {
            ApplyPoisonUpgrade(other.gameObject);
        }
    }

    private void ApplyPoisonUpgrade(GameObject player)
    {
        var playerComponent = player.GetComponent<Player>();
        if (playerComponent == null)
        {
            Debug.LogError("PoisonUpgradeCollider: No se encontró Player en el jugador");
            return;
        }

        var attackSystem = playerComponent.AttackSystem;
        if (attackSystem == null)
        {
            Debug.LogError("PoisonUpgradeCollider: No se encontró AttackSystem en el jugador");
            return;
        }

        var poisonConfig = new PoisonedAttack.PoisonConfig
        {
            tickDamage = poisonTickDamage,
            duration = poisonDuration,
            tickInterval = poisonTickInterval,
            poisonVisualEffectPrefab = poisonVisualEffectPrefab
        };
    
        bool success = attackSystem.ApplyPoisonToMeleeWeapon(poisonConfig);
        
        if (success)
        {
            Debug.Log($"¡UPGRADE APLICADO! Arma envenenada - Daño: {poisonTickDamage}, Duración: {poisonDuration}s");
            Debug.Log($"Estado del arma: {attackSystem.GetMeleeWeaponStatus()}");
            
            hasBeenUsed = true;
            
            if (destroyAfterUse)
            {
                Destroy(gameObject, 0.1f);
            }
        }
        else
        {
            Debug.LogWarning("No se pudo aplicar el upgrade de veneno (posiblemente ya lo tienes)");
        }
    }
} 