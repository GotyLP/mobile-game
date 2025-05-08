using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Collections;

public class RemoteConfig : MonoBehaviour
{
    public static RemoteConfig Instance { get; private set; }

    // Variables de Remote Config
    public bool ShouldShowPrototype { get; private set; }
    public string MenuText { get; private set; }
    public int PlayerScore { get; private set; }
    public float EnemyEnergy { get; private set; }
    public int ActualLevel { get; private set; }
    public float GameDifficulty { get; private set; }

    public struct userAttributes { }
    public struct appAttributes { }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        StartCoroutine(UpdateRemoteData());
    }

    IEnumerator UpdateRemoteData()
    {
        while (true)
        {
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
            yield return new WaitForSeconds(30);
        }
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        // Obtener valores de Remote Config
        ShouldShowPrototype = RemoteConfigService.Instance.appConfig.GetBool("ShouldShowPrototype", false);
        MenuText = RemoteConfigService.Instance.appConfig.GetString("MenuText", "Main Menu");
        PlayerScore = RemoteConfigService.Instance.appConfig.GetInt("PlayerScore", 0);
        EnemyEnergy = RemoteConfigService.Instance.appConfig.GetFloat("EnemyEnergy", 100f);
        ActualLevel = RemoteConfigService.Instance.appConfig.GetInt("ActualLevel", 1);
        GameDifficulty = RemoteConfigService.Instance.appConfig.GetFloat("GameDifficulty", 1f);

        Debug.Log($"Remote Config actualizado - Prototipo: {ShouldShowPrototype}, " +
                 $"Título: {MenuText}, Score: {PlayerScore}, " +
                 $"Energía Enemigo: {EnemyEnergy}, Nivel: {ActualLevel}, " +
                 $"Dificultad: {GameDifficulty}x");
    }
}

