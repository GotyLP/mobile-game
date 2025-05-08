using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.Services.RemoteConfig;

public class MainMenu : MonoBehaviour
{ 
    [Header("UI Elements")]
    [SerializeField] private Button _prototypeButton;
    [SerializeField] private TextMeshProUGUI _menuTitleText;
    [SerializeField] private GameObject _windows;

    private void Start()
    {

        Debug.Log("MainMenu Start - RemoteConfig Instance: " + (RemoteConfig.Instance != null));
        
        if (RemoteConfig.Instance != null)
        {
            StartCoroutine(WaitForRemoteConfig());
        }
        else
        {
            Debug.LogError("RemoteConfig.Instance es null. Asegúrate de que el RemoteConfig esté en la escena y se haya inicializado correctamente.");
        }
    }

    private IEnumerator WaitForRemoteConfig()
    {
        while (string.IsNullOrEmpty(RemoteConfig.Instance.MenuText))
        {
            Debug.Log("Esperando a que RemoteConfig esté listo...");
            yield return new WaitForSeconds(0.1f);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        Debug.Log("Actualizando UI con valores de RemoteConfig");
        
        if (_prototypeButton != null)
        {
            _prototypeButton.gameObject.SetActive(RemoteConfig.Instance.ShouldShowPrototype);
        }
        else
        {
            Debug.LogWarning("Prototype Button no asignado en el Inspector");
        }

        if (_menuTitleText != null)
        {
            Debug.Log("Actualizando texto del menú a: " + RemoteConfig.Instance.MenuText);
            _menuTitleText.text = RemoteConfig.Instance.MenuText;
        }
        else
        {
            Debug.LogWarning("Menu Title Text no asignado en el Inspector");
        }
      
    }

    public void OnPlayButtonClicked()
    {
        int currentLevel = RemoteConfig.Instance.ActualLevel;
        string levelName = $"Level{currentLevel}";
        Debug.Log($"Cargando nivel: {levelName}");
        
        // Desactivar el Canvas
        Canvas menuCanvas = GetComponentInParent<Canvas>();
        if (menuCanvas != null)
        {
            Debug.Log("Desactivando Canvas del menú");
            menuCanvas.gameObject.SetActive(false);
        }
        
        SceneManager.LoadScene(levelName);
    }
    
    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
   
    public void ActivateObject() 
    {
        if (_windows != null)
            _windows.SetActive(true);
    }
   
    public void DeactivateObject()
    {
        if (_windows != null)
            _windows.SetActive(false);
    }

    public void ToggleObject()
    {
        if (_windows != null)
            _windows.SetActive(!_windows.activeSelf);
    }

    public void QuitApplication() 
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();
    }
}
