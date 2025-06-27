using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class SaveData
{
    public static Vector3 playerTransform;
    public static string playerName = "player";
    public static float life ;
    public static float maxLife ;
    public static float posX, posY, posZ;
}
public class SaveDataDto
{
    public  float life;
    public  float maxLife;
    public  Vector3 playerPosition;
    public  string playerName = "player";

    // Add other fields as necessary
}
