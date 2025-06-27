using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject warningMenu;
    [Header("Loading UI")]
    public GameObject loadingPanel; // Panel de carga opcional
    // public GameObject pausePanel;

    private void OnEnable()
    {
        EventManager.Subscribe(SimpleEventType.PlayerDeathEvent, OnPlayerDeath);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(SimpleEventType.PlayerDeathEvent, OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        ActivateObject();
    }

    private void Update()
    {
        OpenMenu();
    }
    
    public void ChangeSceneByIndex(int sceneIndex) // Cambiar escena por Build Settings usando carga asíncrona
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }
    
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        // Mostrar panel de carga si existe
        if (loadingPanel != null)
            loadingPanel.SetActive(true);
            
        // Iniciar carga asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        
        // Configurar prioridad normal para mejor rendimiento
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        
        // Esperar hasta que la escena esté completamente cargada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // Ocultar panel de carga
        if (loadingPanel != null)
            loadingPanel.SetActive(false);
    }
    
    public void ActivateObject()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);        
    }
    public void ToggleObject()
    {
        bool isActive = !pauseMenu.activeSelf;
        pauseMenu.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;
    }
    public void ToggleWarning()
    {
        bool isActive = !warningMenu.activeSelf;
        warningMenu.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;
    }


    public void DeactivateObject()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);        
    }
    public void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleObject();
        }
    }

}
