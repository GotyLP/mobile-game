using UnityEngine;
using UnityEngine.UI;

public class AudioManagerr : MonoBehaviour
{
    public static AudioManagerr Instance;

    [Header("UI Elements")]
    public Slider volumeSlider;
    public Toggle muteToggle;

    private float currentVolume = 1f;
    private bool isMuted = false;

    private const string VolumeKey = "GameVolume";
    private const string MuteKey = "GameMute";

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadSettings();
    }

    void Start()
    {
        // Update UI values
        if (volumeSlider != null)
        {
            volumeSlider.value = currentVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (muteToggle != null)
        {
            muteToggle.isOn = isMuted;
            muteToggle.onValueChanged.AddListener(SetMute);
        }

        ApplyVolume();
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        SaveSettings();
        ApplyVolume();
    }

    public void SetMute(bool mute)
    {
        isMuted = mute;
        SaveSettings();
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        AudioListener.volume = isMuted ? 0 : currentVolume;
    }

    private void LoadSettings()
    {
        currentVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(VolumeKey, currentVolume);
        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
