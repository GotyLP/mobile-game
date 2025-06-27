using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("Loading UI")]
    public GameObject loadingPanel;
    public Image loadingProgressBar;
    public float loadingSpeed = 2f;
    
    private static SceneLoader instance;
    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Carga una escena de forma asíncrona con pantalla de carga
    /// </summary>
    /// <param name="sceneName">Nombre de la escena a cargar</param>
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    /// <summary>
    /// Carga una escena por índice de forma asíncrona
    /// </summary>
    /// <param name="sceneIndex">Índice de la escena en Build Settings</param>
    public void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return StartCoroutine(LoadSceneInternal(() => SceneManager.LoadSceneAsync(sceneName)));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        yield return StartCoroutine(LoadSceneInternal(() => SceneManager.LoadSceneAsync(sceneIndex)));
    }

    private IEnumerator LoadSceneInternal(System.Func<AsyncOperation> loadOperation)
    {
        // Mostrar pantalla de carga
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        if (loadingProgressBar != null)
        {
            loadingProgressBar.fillAmount = 0f;
        }

        // Configurar prioridad de carga para mejor rendimiento
        Application.backgroundLoadingPriority = ThreadPriority.Normal;

        // Iniciar carga asíncrona
        AsyncOperation asyncLoad = loadOperation();

        // Actualizar barra de progreso
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
            if (loadingProgressBar != null)
            {
                loadingProgressBar.fillAmount = Mathf.Lerp(loadingProgressBar.fillAmount, progress, Time.deltaTime * loadingSpeed);
            }

            yield return null;
        }

        // Asegurar que la barra llegue al 100%
        if (loadingProgressBar != null)
        {
            loadingProgressBar.fillAmount = 1f;
        }

        // Pequeño delay para mostrar el 100% antes de cambiar
        yield return new WaitForSeconds(0.1f);

        // Ocultar pantalla de carga
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Método estático para cargar escenas desde cualquier parte del código
    /// </summary>
    /// <param name="sceneName">Nombre de la escena</param>
    public static void LoadScene(string sceneName)
    {
        if (Instance != null)
        {
            Instance.LoadSceneAsync(sceneName);
        }
        else
        {
            // Fallback si no hay instancia disponible
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// Método estático para cargar escenas por índice
    /// </summary>
    /// <param name="sceneIndex">Índice de la escena</param>
    public static void LoadScene(int sceneIndex)
    {
        if (Instance != null)
        {
            Instance.LoadSceneAsync(sceneIndex);
        }
        else
        {
            // Fallback si no hay instancia disponible
            SceneManager.LoadScene(sceneIndex);
        }
    }
} 