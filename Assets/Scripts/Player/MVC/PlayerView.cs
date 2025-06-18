using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Image _lifeBarImage;
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();

        var model = GetComponentInParent<PlayerModel>();
        if (model != null ) return;
        model.OnMovement += MovementAnimation;
        model.OnLifeChange += UpdateLifeBar;
    }
    private void MovementAnimation(float xValue, float yValue)
    {
        //_animator.SetFloat("xAxi", xValue);
        //_animator.SetFloat("yAxi", yValue);
    }
    void UpdateLifeBar(float amount)
    {
        _lifeBarImage.fillAmount = amount;
    }
}
