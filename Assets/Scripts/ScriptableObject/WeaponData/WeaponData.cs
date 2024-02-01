using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Spec")]
    [Range(0, 50)]
    public int damage = 50;
}
