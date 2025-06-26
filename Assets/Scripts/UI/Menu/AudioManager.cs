using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [HideInInspector] public float volumenActual = 1f;
    [HideInInspector] public bool estaMuteado = false;

    private void Awake()
    {
       
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); 

            // Cargar preferencias
            volumenActual = PlayerPrefs.GetFloat("volumen", 1f);
            estaMuteado = PlayerPrefs.GetInt("muteado", 0) == 1;

            AplicarVolumen();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void EstablecerVolumen(float volumen)
    {
        volumenActual = volumen;
        estaMuteado = volumen <= 0f;

        AplicarVolumen();

        
        PlayerPrefs.SetFloat("volumen", volumenActual);
        PlayerPrefs.SetInt("muteado", estaMuteado ? 1 : 0);
    }

    public void EstablecerMute(bool mute)
    {
        estaMuteado = mute;
        AplicarVolumen();

        
        PlayerPrefs.SetFloat("volumen", volumenActual);
        PlayerPrefs.SetInt("muteado", estaMuteado ? 1 : 0);
    }

    private void AplicarVolumen()
    {
        AudioListener.volume = estaMuteado ? 0f : volumenActual;
    }
}
