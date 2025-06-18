using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Transform playerTransform;
    public string playerName = "player";
    public int life = 100;
    public int mana = 100;
}
