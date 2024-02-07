using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int hp = 100;
    public float speed = 15.0f;
}
