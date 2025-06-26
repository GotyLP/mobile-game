using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text TextPoints;
    public Button BuyButton;   

public int cost = 30;

void Start()
{
    ActualizeUi();
}

public void Buy()
{
    if (CurrenciManager.Instance.UsePoints(cost))
    {
        Debug.Log("Compra realizada con éxito.");
    }
    else
    {
        Debug.Log("No tienes suficientes puntos ");
    }

    ActualizeUi();
}

void ActualizeUi()
{
    TextPoints.text = "Coins: " + CurrenciManager.Instance.GetPoints();
}
}

