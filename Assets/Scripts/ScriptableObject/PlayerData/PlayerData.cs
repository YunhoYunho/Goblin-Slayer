using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int hp = 200;
    public float speed = 2f;
}
