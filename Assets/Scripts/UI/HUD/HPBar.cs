using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private UnitHealth health;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.maxValue = health.HP;
        slider.value = health.HP;
    }

    private void Start()
    {
        health.OnHPChanged.AddListener((hp) => { slider.value = hp; });
    }
}
