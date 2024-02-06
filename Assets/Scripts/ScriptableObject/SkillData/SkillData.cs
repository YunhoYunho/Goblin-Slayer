using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Attack, Buff }

[CreateAssetMenu(menuName = "Scriptable/Skill")]
public class SkillData : ScriptableObject
{
    public Sprite icon;
    public float coolTime;
    public string animationName;
}
