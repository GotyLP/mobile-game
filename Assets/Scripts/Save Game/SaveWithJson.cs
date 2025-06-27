using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveWithJson : MonoBehaviour
{
    public Player player;
    public Transform playerTransform; // Asigna el transform del jugador    
    [SerializeField] string _path;
    string _previusPath;
    [SerializeField] string _saveArchiveName;
    [SerializeField] private PlayerModel _playerModel;
    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned in SaveWithJson script. Please assign it in the Inspector.");
            return;
        }
        _playerModel = player.Model;
    }
    private void Awake()
    {
        _path = Application.persistentDataPath;
        _previusPath = _path;
        _path += $"/{_saveArchiveName}";
        LoadData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            SaveGameData();
            Debug.Log("Data Saved: ");
        }
    }
    public void SaveGameData()
    {
        Debug.Log(_playerModel.CurrentLife + " Saving Data...");
        SaveData.life = _playerModel.CurrentLife !=0 ? _playerModel.CurrentLife : 0;
        SaveDataDto saveDataDto = new SaveDataDto
        {
            life = SaveData.life,
            playerPosition = SaveData.playerTransform,
            playerName = SaveData.playerName
        };
        /*
        {
            posX = playerTransform.position.x,
            posY = playerTransform.position.y,
            posZ = playerTransform.position.z,            
        };*/
        Debug.Log("Saving Data...");
        string json = JsonUtility.ToJson(saveDataDto, true);
        Debug.Log(json + "SAVE DATA  ");
       
        PlayerPrefs.SetString("MySaveData", json);
        // File.WriteAllText(_path, json);
    }
    public void LoadData()
    {
        if (Directory.Exists(_previusPath))
        {
            Debug.Log("Loading Data...");
            //playerTransform.position = new Vector3(data.posX, data.posY, data.posZ);
            string json = PlayerPrefs.HasKey("MySaveData") ? PlayerPrefs.GetString("MySaveData") : null;
            SaveDataDto data = JsonUtility.FromJson<SaveDataDto>(json);
            if (data != null)
            {
                SaveData.playerTransform = data.playerPosition;
                SaveData.life = data.life;
                SaveData.playerName = data.playerName;               
                Debug.Log($"Player Position: {SaveData.playerTransform}");
            }
            else
            {
                Debug.LogWarning("No save data found or data is null.");
            }
            //string json = File.ReadAllText(_path);
            Debug.Log(json+" datos cargados    ");
           // JsonUtility.FromJsonOverwrite(json, _mySave);
        }
        EventManager.Trigger(new PlayerHealthChangedEvent(SaveData.life, 100));
        Debug.Log("Load game initialized with current Life: " + SaveData.life);
    }
    public void DeleteData()
    {
        Debug.Log("Data Deleted");
        PlayerPrefs.DeleteKey("MySaveData");
        SaveData.life = 0;
    }

    private void OnApplicationLostFocus(bool focus)
    {
               if (!focus) SaveGameData();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveGameData();
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Saving Data...");
        SaveGameData();
    }
}
