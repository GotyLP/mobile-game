using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
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
    public void ChangeSceneByIndex(int sceneIndex) // Cambiar escena por Build Settings
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
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
