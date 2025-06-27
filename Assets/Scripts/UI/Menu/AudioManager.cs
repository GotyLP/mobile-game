using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    private const string VolumeKey = "Volume";
    private const string MuteKey = "Muted";

    void Start()    {
        
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        bool isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;

        volumeSlider.value = savedVolume;
        muteToggle.isOn = isMuted;

        ApplyVolume(savedVolume, isMuted);

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteToggled);
    }

    void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        ApplyVolume(value, muteToggle.isOn);
    }

    void OnMuteToggled(bool isMuted)
    {
        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);
        ApplyVolume(volumeSlider.value, isMuted);
    }

    void ApplyVolume(float volume, bool isMuted)
    {
        AudioListener.volume = isMuted ? 0f : volume;
    }
}