using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AsyncChargeScene : MonoBehaviour
{
    [SerializeField] Image _loadingImage;
    public StaminaSystem stamina;
    [SerializeField] GameObject _panelLoading, _playGamebutton;
    AsyncOperation _myScene;
    private void Start()
    {
        _loadingImage.fillAmount = 0f;
        _panelLoading.SetActive(false);
        _playGamebutton.SetActive(false);
    }
    public void LoadMyScene( string sceneName)=> StartCoroutine(LoadingMyScene(sceneName));

    IEnumerator LoadingMyScene(string sceneName)
    {
        if(stamina._currentStamina>=3)
        {
            _panelLoading.SetActive(true);
            _myScene = SceneManager.LoadSceneAsync(sceneName);
            LoadLevel(false);

            Application.backgroundLoadingPriority = ThreadPriority.Normal;
            
            while (!_myScene.isDone)
            {
                float progress = Mathf.Clamp01(_myScene.progress / 0.9f);
                _loadingImage.fillAmount = Mathf.Lerp(_loadingImage.fillAmount, progress, Time.deltaTime * 2f);
                
                if (_loadingImage.fillAmount >= 0.99f && _myScene.progress >= 0.9f)
                {
                    _playGamebutton.SetActive(true);
                }
                
                yield return null;
            }
        }
        else
        {
            Debug.Log("Not enough stamina to continue");           
        }


    }
    public void LoadLevel(bool value)=>_myScene.allowSceneActivation = value; // Permite activar la escena una vez que se ha cargado completamente

}
