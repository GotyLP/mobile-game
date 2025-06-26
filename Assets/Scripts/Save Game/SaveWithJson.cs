using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveWithJson : MonoBehaviour
{
    public Transform jugadorTransform; // Asigna el transform del jugador
    [SerializeField] SaveData _mySave = new SaveData();
    [SerializeField] string _path;
    string _previusPath;
    [SerializeField] string _saveArchiveName;
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
            SaveData();
            Debug.Log("Data Saved: ");
        }
    }
    public void SaveData()
    {
        Debug.Log("Saving Data...");
        string json = JsonUtility.ToJson(_mySave, true);
        Debug.Log(json);
       
        PlayerPrefs.SetString("MySaveData", json);
        // File.WriteAllText(_path, json);
    }
    public void LoadData()
    {
        if (Directory.Exists(_previusPath))
        {
            Debug.Log("Loading Data...");
            string json = PlayerPrefs.HasKey("MySaveData") ? PlayerPrefs.GetString("MySaveData") : null;
            //string json = File.ReadAllText(_path);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, _mySave);
        }
    }
    public void DeleteData()
    {
        Debug.Log("Data Deleted");
        PlayerPrefs.DeleteKey("MySaveData");
    }
    private void OnApplicationLostFocus(bool focus)
    {
               if (!focus) SaveData();
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
