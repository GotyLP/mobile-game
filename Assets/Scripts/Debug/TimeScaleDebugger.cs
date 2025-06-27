using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScaleDebugger : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool logTimeScaleChanges = true;
    public bool forceTimeScaleInMenus = true;
    
    private float lastTimeScale = 1f;
    
    private void Start()
    {
        lastTimeScale = Time.timeScale;
        
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"TimeScaleDebugger: Iniciado en escena '{sceneName}' con Time.timeScale = {Time.timeScale}");
        
        // Forzar timeScale a 1 en escenas de menú
        if (forceTimeScaleInMenus && IsMenuScene(sceneName))
        {
            Debug.Log($"TimeScaleDebugger: Forzando Time.timeScale = 1 en escena de menú '{sceneName}'");
            Time.timeScale = 1f;
        }
    }
    
    private void Update()
    {
        // Detectar cambios en Time.timeScale
        if (logTimeScaleChanges && Mathf.Abs(Time.timeScale - lastTimeScale) > 0.001f)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            Debug.LogWarning($"TimeScaleDebugger: Time.timeScale cambió de {lastTimeScale} a {Time.timeScale} en escena '{sceneName}'");
            
            // Imprimir stack trace para identificar qué causó el cambio
            Debug.LogWarning("Stack trace del cambio de Time.timeScale:");
            Debug.LogWarning(System.Environment.StackTrace);
            
            lastTimeScale = Time.timeScale;
            
            // Si estamos en un menú y el timeScale se puso a 0, corregirlo
            if (forceTimeScaleInMenus && IsMenuScene(sceneName) && Time.timeScale < 0.1f)
            {
                Debug.LogError($"TimeScaleDebugger: ¡CORRIGIENDO! Time.timeScale pausado incorrectamente en menú '{sceneName}'. Restaurando a 1.");
                Time.timeScale = 1f;
            }
        }
    }
    
    private bool IsMenuScene(string sceneName)
    {
        return sceneName.Contains("Menu") || 
               sceneName.Contains("MainMenu") || 
               sceneName.Contains("menu") ||
               sceneName == "SampleScene"; // Añadir nombres específicos de escenas de menú aquí
    }
    
    [ContextMenu("Force TimeScale to 1")]
    public void ForceTimeScaleToOne()
    {
        Debug.Log("TimeScaleDebugger: Forzando Time.timeScale = 1");
        Time.timeScale = 1f;
    }
    
    [ContextMenu("Log Current TimeScale")]
    public void LogCurrentTimeScale()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"TimeScaleDebugger: Time.timeScale actual = {Time.timeScale} en escena '{sceneName}'");
    }
} 