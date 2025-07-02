using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged,
}
[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : ScriptableObject
{
    [Header("Datos Básicos")]
    public string weaponName = "Arma";
    public float damage = 10f;
    public float attackCooldown = 0.5f;
    public float energyCost = 10f;
    public AttackType attackType = AttackType.Melee;
    
    [Header("Animación")]
    [Tooltip("Trigger de animación para este arma (ej: Attack01, Attack02)")]
    public string animationTrigger = "Attack01";
    
    [Header("Configuración de Efectos")]
    [Tooltip("Lista de efectos disponibles para esta arma")]
    public List<SlashParameters> attackEffects = new List<SlashParameters>();
    
    [Header("Transform de Arma")]
    [Tooltip("Nombre del transform hijo donde aparecerán los efectos (opcional). Ej: 'WeaponHolder', 'RightHand'")]
    public string weaponTransformName = "";

    [Header("Configuración Avanzada")]
    [Tooltip("Activar ciclado automático de efectos")]
    public bool enableEffectCycling = true;
    
    [Tooltip("Usar efecto específico por ID (deja vacío para usar ciclado)")]
    public string specificEffectID = "";
}