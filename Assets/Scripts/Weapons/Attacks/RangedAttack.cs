using UnityEngine;

public class RangedAttack : IAttackBehavior
{
    public string AttackName => "Ranged Attack";

    public void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        // Lógica específica del ataque a distancia
        Debug.Log($"¡Ataque a distancia de {weaponData.weaponName}! Daño: {weaponData.damage}");
        
        // Crear proyectil o raycast
        CreateProjectile(weaponData, attacker);
        
        PlayAttackEffects(weaponData, attacker);
    }

    public void StopAttack(GameObject damageCollider)
    {
        // Para ataques a distancia, generalmente no hay nada que desactivar
        // A menos que sea un láser continuuo o similar
    }

    private void CreateProjectile(WeaponItem weaponData, Transform attacker)
    {
        // Implementar lógica de proyectil
        // Por ejemplo: instanciar bala, aplicar física, etc.
    }

    private void PlayAttackEffects(WeaponItem weaponData, Transform attacker)
    {
        // Implementar efectos específicos del ataque a distancia
        // Por ejemplo: muzzle flash, sonido de disparo, etc.
    }
} 