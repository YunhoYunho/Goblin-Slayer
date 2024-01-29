using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : UnitHealth
{
    [SerializeField]
    private EnemyData enemyData;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        initHP = enemyData.hp;
        hp = initHP;
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("OnDeath");

        initHP = enemyData.hp;
        hp = enemyData.hp;

        StartCoroutine(ReturnToPool());
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(3f);

        PoolManager.Instance.Release(gameObject);
    }
}
