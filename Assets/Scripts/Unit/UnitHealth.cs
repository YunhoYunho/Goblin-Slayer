using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitHealth : MonoBehaviour, IHittable
{
    [SerializeField]
    protected float initHP;
    [SerializeField]
    protected float hp;
    public float HP { get { return hp; } protected set { hp = value; OnHPChanged?.Invoke(hp); } }
    public bool isDead { get; protected set; }
    public UnityEvent<float> OnHPChanged;
    public UnityEvent OnDied;

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    public virtual void TakeHit(float damage)
    {
        HP -= damage;

        if (HP <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDied?.Invoke();
        isDead = true;
    }
}
