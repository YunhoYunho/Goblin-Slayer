using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Range(0, 200)]
    public int hp = 100;
    [Range(0f, 15.0f)]
    public float speed = 15.0f;
    [Range(0f, 50.0f)]
    public float traceDist = 30.0f;
    [Range(0f, 50.0f)]
    public float attackDist = 20.0f;
}
