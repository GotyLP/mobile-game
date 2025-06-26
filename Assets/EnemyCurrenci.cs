using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurrenci : MonoBehaviour
{
    public int puntosAlMorir = 10;

    void OnDestroy()
    {
        if (CurrenciManager.Instance != null)
        {
            Debug.Log("RECOMPESA GANADA!!!: " + puntosAlMorir + " puntos al morir " + gameObject.name);
            CurrenciManager.Instance.AddPoints(puntosAlMorir);
        }
    }
}
