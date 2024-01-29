using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitHealth : MonoBehaviour, IHittable
{
    [SerializeField]
    protected int initHP;
    [SerializeField]
    protected int hp;
    public int HP { get { return hp; } protected set { hp = value; OnHPChanged?.Invoke(hp); } }
    public bool isDead { get; protected set; }
    public UnityEvent<int> OnHPChanged;
    public UnityEvent OnDied;

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    public virtual void TakeHit(int damage)
    {
        HP -= damage;

        if (HP <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void RestoreHP(int amount)
    {
        if (isDead)
            return;

        if (HP <= initHP)
        {
            HP += amount;

            if (HP > initHP)
            {
                HP = initHP;
            }
        }
    }

    public virtual void Die()
    {
        OnDied?.Invoke();
        isDead = true;
    }
}
