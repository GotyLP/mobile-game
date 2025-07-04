using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public enum Language
{
    Spanish,
    English
}

public class Localization : MonoBehaviour
{
    [SerializeField] private string _webUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vThml7khYzwte4kIJMzY08hUqice30fjunsljdkw2Qem8jveXyIKrbi9xp4SPM4Dc7QSPq1RrvgvLzR/pub?output=csv";

    [SerializeField] private Language _currentLang;

    private Dictionary<Language, Dictionary<string, string>> _localization;
    
    public event Action OnUpdate;

    private void Awake()
    {
        StartCoroutine(DownloadCsv(_webUrl));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _currentLang = _currentLang == Language.English ? Language.Spanish : Language.English;

            OnUpdate?.Invoke();
        }
    }
    public void SetLanguage()
    {
        _currentLang = _currentLang == Language.English ? Language.Spanish : Language.English;

        OnUpdate?.Invoke();
    }

    IEnumerator DownloadCsv(string url)
    {
        var www = new UnityWebRequest(url);

        www.downloadHandler = new DownloadHandlerBuffer();

        //www.Abort();
        
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var textResult = www.downloadHandler.text;

            _localization = LanguageSplit.LoadCsv(textResult, "url");
            
            SaveText(fileName: "Localization.txt", content: textResult);
        }
        else
        {
            var textResult = LoadText(fileName: "Localization.txt");
            _localization = LanguageSplit.LoadCsv(textResult, "local");
        }
        
        OnUpdate?.Invoke();
    }

    public string GetTranslate(string Id)
    {
        var idsDictionary = _localization[_currentLang];

        idsDictionary.TryGetValue(Id, out var result);

        return result;
    }

    void SaveText(string fileName, string content)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        try
        {
            File.WriteAllText(path, content);
            Debug.LogWarning($"File saved successfully at: {path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save file - {e}");
        }
    }

    string LoadText(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;
        
        try
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Debug.LogWarning($"File loaded successfully from: {path}");
                return content;
            }
            else
            {
                Debug.LogWarning($"File not found at: {path}");
                return default;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load file - {e}");
            return default;
        }
    }
}
