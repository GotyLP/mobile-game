using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class StaminaSystem : MonoBehaviour
{
    // Reloj
    DateTime _nextStaminaTime; 
    DateTime _lastStaminaTime;

    [SerializeField] int _maxStamina = 10;   
    int _currentStamina = 10;

    [SerializeField] float _timeToRecharge = 10;
    [SerializeField] TextMeshProUGUI _staminaText,_timerText;

    bool _recharging;
    TimeSpan notifTimer;
    int _id;
    void Start()
    {
        LoadGame();
        StartCoroutine(ChargingStamina());
        if(_currentStamina < _maxStamina)
        {
            notifTimer = _nextStaminaTime - DateTime.Now;
            DisplayNotif();
        }
        
    }
    IEnumerator ChargingStamina()
    {
        UpdateStamina();
        UpdateTimer();

        _recharging = true;
        while (_currentStamina < _maxStamina)
        {
            DateTime current = DateTime.Now;
            DateTime nextTime = _nextStaminaTime;
            bool addingStamina = false;

            while (current > nextTime)
            {
                if (_currentStamina >= _maxStamina) break;
                {
                    _currentStamina++;                   
                    addingStamina = true;
                    UpdateStamina();

                    DateTime timeToAdd = nextTime;

                    if(_lastStaminaTime > nextTime) timeToAdd = _lastStaminaTime;

                    nextTime = AddDuration (timeToAdd, _timeToRecharge);
                }
            }
            if (addingStamina)
            {
                _nextStaminaTime = nextTime;
                _lastStaminaTime = DateTime.Now;              
            }
            UpdateTimer();
            UpdateStamina();
            SaveGame();
            yield return new WaitForEndOfFrame();

        }
        MobileNotificationManager.Instance.CancelNotification(_id);
        _recharging = false;
    }
    DateTime AddDuration(DateTime timeToAdd, float duration) => timeToAdd.AddSeconds(duration);

    public bool HasEnoughStamina(int stamina) => _currentStamina - stamina >= 0;
    public void UseStamina(int quantityOfUsage)
    {
        if(HasEnoughStamina(quantityOfUsage))
        {
            _currentStamina -= quantityOfUsage;
            UpdateStamina();
           MobileNotificationManager.Instance.CancelNotification(_id);
            DisplayNotif();

            if (!_recharging)
            {
                _nextStaminaTime = AddDuration(DateTime.Now, _timeToRecharge);
                StartCoroutine(ChargingStamina());
            }
            else
            {
                Debug.Log("Stamina is recharging, please wait.");
            }
        }
        
    }
    public void DisplayNotif()
    {
        _id = MobileNotificationManager.Instance.DisplayNotification("Stamina Recharged!", "Your stamina has been recharged, come back and play!",
           IconSelecter.icon_0, IconSelecter.icon_1, AddDuration(DateTime.Now, ((_maxStamina - (_currentStamina) + 1 * _timeToRecharge) + 1 
           +(float) notifTimer.TotalSeconds)));
        
    }

    void UpdateTimer()
    {
        if (_currentStamina >= _maxStamina)
        {
            _timerText.text = "Full Stamina!";
            return;
        }

        notifTimer = _nextStaminaTime - DateTime.Now;

        _timerText.text = $"{notifTimer.Minutes.ToString("00")} : {notifTimer.Seconds.ToString("00")}";
    }
    void UpdateStamina()
    {
        _staminaText.text = $"Stamina: {_currentStamina}/{_maxStamina}";
    }
    void SaveGame()
    {
        PlayerPrefs.SetInt("CurrentStamina", _currentStamina);
        PlayerPrefs.SetString("NextStaminaTime", _nextStaminaTime.ToString());
        PlayerPrefs.SetString("LastStaminaTime", _lastStaminaTime.ToString());
    }
    void LoadGame()
    {
        _currentStamina = PlayerPrefs.GetInt("CurrentStamina", _maxStamina);        
        _nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("NextStaminaTime"));
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));      
        UpdateStamina();       
    }
    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date)) 
            return DateTime.Now;
        else
            return DateTime.Parse(date);
    }
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveGame();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
