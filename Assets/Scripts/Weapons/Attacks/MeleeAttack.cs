using UnityEngine;

public class MeleeAttack : IAttackBehavior
{
    public string AttackName => "Melee Attack";

    public void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        if (damageCollider != null)
        {
            damageCollider.SetActive(true);
            
            // Lógica específica del ataque cuerpo a cuerpo
            Debug.Log($"¡Ataque cuerpo a cuerpo de {weaponData.weaponName}! Daño: {weaponData.damage}");
            
            // Aquí podrías agregar efectos de partículas, sonidos, etc.
            PlayAttackEffects(weaponData, attacker);
        }
    }

    public void StopAttack(GameObject damageCollider)
    {
        if (damageCollider != null)
        {
            damageCollider.SetActive(false);
        }
    }

    private void PlayAttackEffects(WeaponItem weaponData, Transform attacker)
    {
        // Implementar efectos específicos del ataque cuerpo a cuerpo
        // Por ejemplo: partículas, sonidos, screen shake, etc.
    }
} 