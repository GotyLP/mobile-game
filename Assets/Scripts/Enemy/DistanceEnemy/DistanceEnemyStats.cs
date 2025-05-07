using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DistanceEnemyStats", menuName = "Enemies/DistanceEnemyStats")]

public class DistanceEnemyStats : ScriptableObject
{
    public float speedMov = 5f;
    public float speedReg = 2f;
    public float maxEnergy = 100f;
    public float maxVelocity = 10f;
    public float radiusPersuit = 10f;
    public float radiusAttack = 2f;
    public float maxForce = 5f;
    public float prediction = 1f;
    public float shootCooldown = 1f;
    public GameObject bulletPrefab;
}