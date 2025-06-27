using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text TextPoints;
    public Button BuyButton;
    public AudioClip sound1; 
    public AudioClip sound2; 

    private AudioSource audioSource;

    public int cost = 30;

void Start()
{
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        ActualizeUi();       
    }

public void Buy()
{
    if (CurrenciManager.Instance.UsePoints(cost))
    {
            audioSource.clip = sound1;
            audioSource.Play();
            Debug.Log("Compra realizada con éxito.");
    }
    else
    {
            audioSource.clip = sound2;
            audioSource.Play();
            Debug.Log("No tienes suficientes puntos ");
    }

    ActualizeUi();
}

void ActualizeUi()
{
    TextPoints.text = "Coins: " + CurrenciManager.Instance.GetPoints();
}
}

