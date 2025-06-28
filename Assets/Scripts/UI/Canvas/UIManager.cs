using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image HP_Slider;
    void OnEnable()
    {
        EventManager.Subscribe<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
    }

    void OnDisable()
    {
        EventManager.Unsubscribe<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
    }

    void OnPlayerHealthChanged(PlayerHealthChangedEvent evt)
    {
        float fill = (float)evt.currentHealth / evt.maxHealth;
        HP_Slider.fillAmount = fill;
    }
}
