using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlashParameters
{
    [Header("Efecto Visual")]
    public GameObject slashEffect;
    
    [Header("Timing")]
    [Tooltip("Tiempo antes de mostrar el efecto (en segundos)")]
    public float delay = 0.2f;
    
    [Header("Seguimiento del Arma")]
    [Tooltip("El efecto sigue la posición del arma durante la animación")]
    public bool followPosition = true;
    [Tooltip("El efecto sigue la rotación del arma durante la animación")]
    public bool followRotation = true;
    [Tooltip("Duración del seguimiento (0 = toda la duración del efecto)")]
    public float followDuration = 0.5f;
    
    [Header("Offset")]
    [Tooltip("Desplazamiento desde la posición del arma")]
    public Vector3 positionOffset = Vector3.zero;
    [Tooltip("Rotación adicional del efecto")]
    public Vector3 rotationOffset = Vector3.zero;
    
    [Header("Configuración Avanzada")]
    [Tooltip("Duración total del efecto visual")]
    public float effectDuration = 2f;
    [Tooltip("Escala del efecto")]
    public Vector3 effectScale = Vector3.one;
    [Tooltip("ID único del efecto")]
    public string effectID = "default";
} 