using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public CurrenciManager CurrenciManager;
    public void LoadGameAndAddLife()
    {       
       if(CurrenciManager.Points >= 30)
       {
            SceneManager.sceneLoaded += Life;
       }
       else
       {
            Debug.Log("No tienes suficientes Puntos .");
       }
    }
    public void LoadGameAndAddSpeed()
    {
        if (CurrenciManager.Points >= 30)
        {
            SceneManager.sceneLoaded += Speed;
        }
        else
        {
            Debug.Log("No tienes suficientes Puntos .");
        }
    }

    public void Life (Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: ");
        // Busca al Player y le llama AddSpeed
        GameObject playerObj = GameObject.Find("character_player");
        if (playerObj != null)
        {
            Debug.Log("Player encontrado en la escena: ");
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("mas vidaaaaaaaaaaaaa: ");
                player.UpdateLife();
            }
        }

        // Desuscribirse para evitar múltiples llamadas
        SceneManager.sceneLoaded -= Life;
    }
    public void Speed(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: ");
        // Busca al Player y le llama AddSpeed
        GameObject playerObj = GameObject.Find("character_player");
        if (playerObj != null)
        {
            Debug.Log("Player encontrado en la escena: ");
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("mas velocidad: ");
                player.UpdateSpeed();
            }
        }

        // Desuscribirse para evitar múltiples llamadas
        SceneManager.sceneLoaded -= Speed;
    }


}
