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
        // Aquí puedes activar lo comprado (ej: desbloquear personaje)
    }
    else
    {
        Debug.Log("No tienes suficientes puntos para esta compra.");
    }

    ActualizeUi();
}

void ActualizeUi()
{
    TextPoints.text = "Puntos: " + CurrenciManager.Instance.GetPoints();
}
}

