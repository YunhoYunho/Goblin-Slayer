using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Consumable }

[CreateAssetMenu(menuName = "Scriptable/Item")]
public class ItemData : ScriptableObject
{
    [Header("Common")]
    public ItemType type;
    public string itemName;
    public Sprite image;
    public bool isStackable = true;
    public int itemCost;
    public float itemAbilityValue;
}
