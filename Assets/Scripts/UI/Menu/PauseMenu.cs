using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    // public GameObject pausePanel;

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
        if (pauseMenu != null)
            pauseMenu.SetActive(!pauseMenu.activeSelf);
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
