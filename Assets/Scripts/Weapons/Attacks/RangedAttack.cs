using UnityEngine;

public class RangedAttack : IAttackBehavior
{
    public string AttackName => "Ranged Attack";

    public void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {        
        Debug.Log($"¡Ataque a distancia de {weaponData.weaponName}! Daño: {weaponData.damage}");
              
        CreateProjectile(weaponData, attacker);
        
        PlayAttackEffects(weaponData, attacker);
    }

    public void StopAttack(GameObject damageCollider)
    {
     
    }

    private void CreateProjectile(WeaponItem weaponData, Transform attacker)
    {
     
    }

    private void PlayAttackEffects(WeaponItem weaponData, Transform attacker)
    {
       
    }
} 