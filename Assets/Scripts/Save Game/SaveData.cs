using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Transform playerTransform;
    public string playerName = "player";
    public int life ;
    public int mana;
    public int Points;
    public float posX, posY, posZ;
}
