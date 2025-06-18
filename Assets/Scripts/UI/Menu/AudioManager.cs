using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("UI Controls")]
    public Slider volumeSlider;
    public Toggle muteToggle;

    private float currentVolume = 1f;
    private bool isMuted = false;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }

        // Listen to scene changes to rebind UI controls
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        ApplySettings();
        BindUIControls(); // Initial binding if UI is in first scene
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindUIControls(); // Rebinds UI controls on scene load
    }

    private void BindUIControls()
    {
        // Find UI elements in the scene (they might not exist in every scene)
        volumeSlider = GameObject.FindWithTag("VolumeSlider")?.GetComponent<Slider>();
        muteToggle = GameObject.FindWithTag("MuteToggle")?.GetComponent<Toggle>();

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.value = currentVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (muteToggle != null)
        {
            muteToggle.onValueChanged.RemoveAllListeners();
            muteToggle.isOn = isMuted;
            muteToggle.onValueChanged.AddListener(SetMute);
        }
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        AudioListener.volume = isMuted ? 0f : currentVolume;
        PlayerPrefs.SetFloat("volume", currentVolume);
        PlayerPrefs.Save();
    }

    public void SetMute(bool mute)
    {
        isMuted = mute;
        AudioListener.volume = isMuted ? 0f : currentVolume;
        PlayerPrefs.SetInt("mute", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        currentVolume = PlayerPrefs.GetFloat("volume", 1f);
        isMuted = PlayerPrefs.GetInt("mute", 0) == 1;
    }

    private void ApplySettings()
    {
        AudioListener.volume = isMuted ? 0f : currentVolume;
    }
}
