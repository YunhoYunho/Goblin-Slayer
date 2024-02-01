using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Attack, Buff }

[CreateAssetMenu(menuName = "Scriptable/Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get { return icon; } }
    [SerializeField]
    private float coolTime;
    public float CoolTime { get {  return coolTime; } }
    [SerializeField]
    private string animationName;
    public string AnimationName { get { return animationName; } }
}
