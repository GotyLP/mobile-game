using UnityEngine;

public class MeleeAttack : IAttackBehavior
{
    public string AttackName => "Melee Attack";
    
    private AttackEffectController _effectController;

    public void ExecuteAttack(WeaponItem weaponData, GameObject damageCollider, Transform attacker)
    {
        Debug.Log($"MeleeAttack: Ejecutando ataque con collider: {damageCollider?.name ?? "NULL"}");
        
        if (damageCollider != null)
        {
            Debug.Log($"MeleeAttack: Activando DamageCollider - Estado anterior: {damageCollider.activeInHierarchy}");
            damageCollider.SetActive(true);
            Debug.Log($"MeleeAttack: DamageCollider activado - Estado actual: {damageCollider.activeInHierarchy}");
            
            Debug.Log($"¡Ataque cuerpo a cuerpo de {weaponData.weaponName}! Daño: {weaponData.damage}");
                
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
        Debug.Log($"MeleeAttack: PlayAttackEffects iniciado para arma: {weaponData.weaponName}");
        
        if (_effectController == null)
        {
            MonoBehaviour coroutineRunner = attacker.GetComponent<MonoBehaviour>();
            if (coroutineRunner != null)
            {
                _effectController = new AttackEffectController(coroutineRunner);
                Debug.Log("MeleeAttack: AttackEffectController creado exitosamente");
            }
            else
            {
                Debug.LogError("MeleeAttack: No se pudo obtener MonoBehaviour del atacante!");
                return;
            }
        }

        if (weaponData.attackEffects != null && weaponData.attackEffects.Count > 0)
        {
            Debug.Log($"MeleeAttack: Encontrados {weaponData.attackEffects.Count} efectos configurados");
            
            Transform weaponTransform = FindWeaponTransform(attacker, weaponData);
            Debug.Log($"MeleeAttack: WeaponTransform encontrado: {(weaponTransform != null ? weaponTransform.name : "NULL")}");
            
            _effectController.Initialize(weaponData.attackEffects, weaponTransform, attacker);
            Debug.Log("MeleeAttack: AttackEffectController inicializado");
           
            if (!string.IsNullOrEmpty(weaponData.specificEffectID))
            {
                Debug.Log($"MeleeAttack: Ejecutando efecto específico: {weaponData.specificEffectID}");
                _effectController.ExecuteEffectByID(weaponData.specificEffectID, weaponData);
            }
            else
            {
                Debug.Log("MeleeAttack: Ejecutando efecto por defecto");
                _effectController.ExecuteEffect(weaponData);
            }
        }
        else
        {
            Debug.LogWarning($"MeleeAttack: No hay efectos configurados para {weaponData.weaponName}. attackEffects: {(weaponData.attackEffects != null ? $"Count={weaponData.attackEffects.Count}" : "NULL")}");
        }
    }

    private Transform FindWeaponTransform(Transform attacker, WeaponItem weaponData)
    {
        Transform weaponTransform = null;
        
        if (!string.IsNullOrEmpty(weaponData.weaponTransformName))
        {
            weaponTransform = attacker.Find(weaponData.weaponTransformName);
            if (weaponTransform != null)
            {
                Debug.Log($"MeleeAttack: Usando transform específico configurado: {weaponData.weaponTransformName}");
                return weaponTransform;
            }
            else
            {
                Debug.LogWarning($"MeleeAttack: No se encontró el transform '{weaponData.weaponTransformName}', usando búsqueda automática");
            }
        }
        
        weaponTransform = attacker.Find("WeaponHolder");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("RightHand");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("Hand_R");
        if (weaponTransform == null)
            weaponTransform = attacker.Find("mixamorig:RightHand");
            
        if (weaponTransform == null)
        {
            Debug.Log("MeleeAttack: No se encontró transform específico de arma, usando transform del atacante");
            weaponTransform = attacker;
        }
        
        return weaponTransform;
    }
} 