using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image HP_Slider;
    private float _maxLife;
    private float _currentLife;
    private void Start()
    {
        _currentLife = SaveData.life != 0 ? SaveData.life : 100f;
        _maxLife = 100;
        float fill = _currentLife/ _maxLife;
        HP_Slider.fillAmount = fill;
    }

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
