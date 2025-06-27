using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrenciManager : MonoBehaviour
{
    public static CurrenciManager Instance;

    public int Points = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int Value)
    {
        Points += Value;
        Debug.Log("Puntos ganados. Total actual: " + Points);
    }

    public bool UsePoints(int Value)
    {
        if (Points >= Value)
        {
            Points -= Value;
            Debug.Log("Puntos gastados. Total restante: " + Points);
            return true;
        }
        else
        {
            Debug.Log("No hay suficientes puntos.");
            return false;
        }
    }

    public int GetPoints()
    {
        return Points;
    }
}
