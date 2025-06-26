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
    public GameObject[] Windows;
    private int windowsIndex = -1;

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
        //Canvas menuCanvas = GetComponentInParent<Canvas>();
        //if (menuCanvas != null)
        //{
        //    Debug.Log("Desactivando Canvas del menú");
        //    menuCanvas.gameObject.SetActive(false);
        //}
        
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
