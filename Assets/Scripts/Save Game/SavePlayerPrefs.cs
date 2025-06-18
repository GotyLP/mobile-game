using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    [SerializeField] int _currency = 10;
    [SerializeField] float _life = 10;
    [SerializeField] string _playerName = "Wachin";
    private void Awake()
    {
           LoadData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Menu)) 
        {
            SaveData();
            Debug.Log("Data Saved: ");
        }
    }

    public void SaveData()
    {
        Debug.Log("Saving Data...");
        PlayerPrefs.SetInt(PlayerPrefKeys.KeyCurrency, _currency);
        PlayerPrefs.SetFloat(PlayerPrefKeys.KeyLife, _life);
        PlayerPrefs.SetString(PlayerPrefKeys.KeyPlayerName, _playerName); 

        PlayerPrefs.Save(); 
    }
    public void LoadData()
    {
        Debug.Log("Loading Data...");
        _currency = PlayerPrefs.GetInt("CurrencyKey", _currency);
        _life = PlayerPrefs.GetFloat(PlayerPrefKeys.KeyLife, PlayerPrefKeys.defaultValueLife);         
        _playerName = PlayerPrefs.GetString("playerNameKey", _playerName);
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("CurrencyKey");
        PlayerPrefs.DeleteAll();
        Debug.Log("Data Deleted");
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();  
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Saving Data...");
        SaveData();
    }
}

