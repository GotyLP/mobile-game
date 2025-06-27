using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _prototypeButton;
    [SerializeField] private TextMeshProUGUI _menuTitleText;
    [SerializeField] private GameObject _windowsSettings;
    [SerializeField] private GameObject _windowsShop;
    [SerializeField] private GameObject _windowsMenu;
    [SerializeField] private GameObject _windowsWarning;
    public GameObject[] Windows;
    private int windowsIndex = -1;

    [Header("Default Values (cuando RemoteConfig no está disponible)")]
    [SerializeField] private bool _defaultShowPrototype = false;
    [SerializeField] private string _defaultMenuText = "Main Menu";
    [SerializeField] private int _defaultLevel = 1;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        Debug.Log("MainMenu Start - RemoteConfig Instance: " + (RemoteConfig.Instance != null));
        
        if (RemoteConfig.Instance != null)
        {
            Debug.Log("RemoteConfig encontrado, esperando inicialización...");
            StartCoroutine(WaitForRemoteConfig());
        }
        else
        {
            Debug.LogWarning("RemoteConfig.Instance es null. Usando valores por defecto del Inspector.");
            UpdateUIWithDefaults();
        }
    }

    private IEnumerator WaitForRemoteConfig()
    {
        // Esperar un tiempo máximo para evitar bucles infinitos
        float maxWaitTime = 5f;
        float waitedTime = 0f;
        
        while (string.IsNullOrEmpty(RemoteConfig.Instance.MenuText) && waitedTime < maxWaitTime)
        {
            Debug.Log("Esperando a que RemoteConfig esté listo...");
            yield return new WaitForSeconds(0.1f);
            waitedTime += 0.1f;
        }

        if (waitedTime >= maxWaitTime)
        {
            Debug.LogWarning("RemoteConfig tardó demasiado en cargar. Usando valores por defecto.");
            UpdateUIWithDefaults();
        }
        else
        {
            UpdateUI();
        }
    }

    private void UpdateUIWithDefaults()
    {
        Debug.Log("Actualizando UI con valores por defecto");
        
        if (_prototypeButton != null)
        {
            _prototypeButton.gameObject.SetActive(_defaultShowPrototype);
        }
        else
        {
            Debug.LogWarning("Prototype Button no asignado en el Inspector");
        }

        if (_menuTitleText != null)
        {
            Debug.Log("Actualizando texto del menú a valor por defecto: " + _defaultMenuText);
            _menuTitleText.text = _defaultMenuText;
        }
        else
        {
            Debug.LogWarning("Menu Title Text no asignado en el Inspector");
        }
    }

    public void WindowsToggle(int _windowsIndex)
    {
        if (_windowsIndex < 0 || _windowsIndex >= Windows.Length)
        {
            Debug.LogWarning("Índice fuera de rango");
            return;
        }

        
        if (windowsIndex == _windowsIndex) // Si el objeto ya está activo lo desactiva
        {
            Windows[_windowsIndex].SetActive(false);
            windowsIndex = -1;
        }
        else
        {
            // Desactiva el objeto anterior si existe
            if (windowsIndex >= 0)
            {
                Windows[windowsIndex].SetActive(false);
            }

            // Activa el nuevo objeto
            Windows[_windowsIndex].SetActive(true);
            windowsIndex = _windowsIndex;
        }
    }
    public void CloseAllWindows()
    {
        _windowsSettings.SetActive(false);
        _windowsShop.SetActive(false);
        _windowsWarning.SetActive(false);
    }
    public void OpenWarning()
    {       
        _windowsWarning.SetActive(true);
    }
    public void CloseWarning()
    {
       
        _windowsWarning.SetActive(false);
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
        // Usar valor por defecto si RemoteConfig no está disponible
        int currentLevel = RemoteConfig.Instance != null ? RemoteConfig.Instance.ActualLevel : _defaultLevel;
        string levelName = $"Level{currentLevel}";
        Debug.Log($"Cargando nivel: {levelName}");
        
        SceneManager.LoadScene(levelName);
    }
    public void ToggleCanvasMenu()
    {
        if (_windowsMenu != null)
            _windowsMenu.SetActive(!_windowsMenu.activeSelf);
    }
    public void QuitApplication() 
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();
    }
}
