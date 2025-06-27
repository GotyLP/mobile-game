using UnityEngine;

public class MeleeAttack : IAttackBehavior
{
    public string AttackName => "Melee Attack";

    public void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        Debug.Log($"MeleeAttack: Ejecutando ataque con collider: {damageCollider?.name ?? "NULL"}");
        
        if (damageCollider != null)
        {
            Debug.Log($"MeleeAttack: Activando DamageCollider - Estado anterior: {damageCollider.activeInHierarchy}");
            damageCollider.SetActive(true);
            Debug.Log($"MeleeAttack: DamageCollider activado - Estado actual: {damageCollider.activeInHierarchy}");
            
            // Lógica específica del ataque cuerpo a cuerpo
            Debug.Log($"¡Ataque cuerpo a cuerpo de {weaponData.weaponName}! Daño: {weaponData.damage}");
            
            // Aquí podrías agregar efectos de partículas, sonidos, etc.
            PlayAttackEffects(weaponData, attacker);
        }
        else
        {
            Debug.LogError("MeleeAttack: DamageCollider es NULL! No se puede activar el collider de daño.");
        }
    }

    public void StopAttack(GameObject damageCollider)
    {
        if (damageCollider != null)
        {
            Debug.Log($"MeleeAttack: Desactivando DamageCollider - Estado anterior: {damageCollider.activeInHierarchy}");
            damageCollider.SetActive(false);
            Debug.Log($"MeleeAttack: DamageCollider desactivado - Estado actual: {damageCollider.activeInHierarchy}");
        }
        else
        {
            Debug.LogError("MeleeAttack: DamageCollider es NULL! No se puede desactivar el collider de daño.");
        }
    }

    private void PlayAttackEffects(WeaponItem weaponData, Transform attacker)
    {
        // Implementar efectos específicos del ataque cuerpo a cuerpo
        // Por ejemplo: partículas, sonidos, screen shake, etc.
    }
} 