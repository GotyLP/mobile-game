using TMPro;
using UnityEngine;

public class TextTranslate : MonoBehaviour
{
    [SerializeField] private string _id;

    [SerializeField] private Localization _localization;

    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        _localization.OnUpdate += ChangeLang;
    }

    void ChangeLang()
    {
        _text.text = _localization.GetTranslate(_id);
    }
    
    private void OnDestroy()
    {
        _localization.OnUpdate -= ChangeLang;
    }
}
