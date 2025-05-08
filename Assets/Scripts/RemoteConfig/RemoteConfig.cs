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
        Debug.Log("RemoteConfig Awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("RemoteConfig Instance creada");
        }
        else
        {
            Debug.Log("RemoteConfig Instance ya existe, destruyendo duplicado");
            Destroy(gameObject);
        }
    }

    async Task InitializeRemoteConfigAsync()
    {
        Debug.Log("Inicializando RemoteConfig...");
        await UnityServices.InitializeAsync();
        Debug.Log("UnityServices inicializado");

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Iniciando sesión anónima...");
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sesión anónima iniciada");
        }
    }

    async Task Start()
    {
        Debug.Log("RemoteConfig Start");
        if (Utilities.CheckForInternetConnection())
        {
            Debug.Log("Conexión a internet detectada, inicializando RemoteConfig");
            await InitializeRemoteConfigAsync();
        }
        else
        {
            Debug.LogWarning("No hay conexión a internet, usando valores por defecto");
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        StartCoroutine(UpdateRemoteData());
    }

    IEnumerator UpdateRemoteData()
    {
        while (true)
        {
            Debug.Log("Actualizando datos de RemoteConfig...");
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
            yield return new WaitForSeconds(30);
        }
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("Aplicando configuración remota...");
        
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

