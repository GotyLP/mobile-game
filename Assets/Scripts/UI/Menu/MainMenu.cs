using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    
    public GameObject Windows;

    
    public void ChangeSceneByName(string sceneName) // Cambiar escena por nombre
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void ChangeSceneByIndex(int sceneIndex) // Cambiar escena por Build Settings
    {
        SceneManager.LoadScene(sceneIndex);
    }
   
    public void ActivateObject() 
    {
        if (Windows != null)
            Windows.SetActive(true);
    }
   
    public void DeactivateObject()
    {
        if (Windows != null)
            Windows.SetActive(false);
    }

    public void ToggleObject()// Alterna entre activo/inactivo
    {
        if (Windows != null)
            Windows.SetActive(!Windows.activeSelf);
    }
    public void QuitApplication() 
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();
    }
}
