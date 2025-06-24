using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour
{
    public Slider sliderVolumen;
    public Toggle toggleMute;

    void Start()
    {
      
        if (sliderVolumen != null)
        {
            sliderVolumen.value = AudioManager.instancia.volumenActual;
            sliderVolumen.onValueChanged.AddListener(AudioManager.instancia.EstablecerVolumen);
        }

        if (toggleMute != null)
        {
            toggleMute.isOn = AudioManager.instancia.estaMuteado;
            toggleMute.onValueChanged.AddListener(AudioManager.instancia.EstablecerMute);
        }
    }
}
