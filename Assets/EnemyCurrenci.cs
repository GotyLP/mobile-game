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
            CurrenciManager.Instance.AddPoints(puntosAlMorir);
        }
    }
}
