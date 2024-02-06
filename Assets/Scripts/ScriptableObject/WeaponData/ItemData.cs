using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Consumable, None }
public enum PotionType { HP, AttackSpped, None }

[CreateAssetMenu(menuName = "Scriptable/Item")]
public class ItemData : ScriptableObject
{
    [Header("Common")]
    public ItemType itemType;
    public PotionType potionType;
    public string itemName;
    public Sprite image;
    public bool isStackable = true;
    public int itemCost;
    public float abilityValue;
    public float during;
}
