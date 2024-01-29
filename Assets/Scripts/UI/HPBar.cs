using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth health;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = health.HP;
        slider.value = health.HP;
        health.OnHPChanged.AddListener((hp) => { slider.value = hp; });
    }
}
