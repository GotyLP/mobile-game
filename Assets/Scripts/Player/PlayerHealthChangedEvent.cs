using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerHealthChangedEvent
{
    public float currentHealth;
    public float maxHealth;

    public PlayerHealthChangedEvent(float current, float max)
    {
        Debug.Log("PlayerHealthChangedEvent");
        currentHealth = current;
        maxHealth = max;
    }
}