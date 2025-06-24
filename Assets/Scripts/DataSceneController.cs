using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSceneController : MonoBehaviour
{
   
    public static DataSceneController Instance;
    public LifeHandler lifeHandler;
    [SerializeField]private float life;
    private void Awake()
    {
        life = lifeHandler._currentLife;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }            
        }
    }
}
