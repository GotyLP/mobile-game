using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AsyncChargeScene : MonoBehaviour
{
    [SerializeField] Image _loadingImage;
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
        _panelLoading.SetActive(true);
        _myScene = SceneManager.LoadSceneAsync(sceneName);
        LoadLevel(false);

        Application.backgroundLoadingPriority = ThreadPriority.Low;
        while (!_myScene.isDone)
        {
            float progress = Mathf.Clamp01(_myScene.progress / 0.9f);
            yield return new  WaitForEndOfFrame();
            _loadingImage.fillAmount = Mathf.MoveTowards(_loadingImage.fillAmount, progress, Time.deltaTime);
            if(_loadingImage.fillAmount >= 1)
            {
                _playGamebutton.SetActive(true);
            }
        }

    }
    public void LoadLevel(bool value)=>_myScene.allowSceneActivation = value; // Permite activar la escena una vez que se ha cargado completamente

}
