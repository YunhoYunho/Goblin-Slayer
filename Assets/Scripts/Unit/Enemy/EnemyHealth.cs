using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Normal, Boss }

public class EnemyHealth : UnitHealth
{
    [SerializeField]
    private EnemyType enemyType;
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private GameObject gemPrefab;

    private GetPoolObject getPool;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        getPool = GameObject.Find("GetPoolObject").GetComponent<GetPoolObject>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        initHP = enemyData.hp;
        hp = initHP;
    }

    public override void TakeHit(float damage)
    {
        if (isDead)
            return;

        base.TakeHit(damage);
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("OnDeath");

        if (enemyType == EnemyType.Normal)
        {
            getPool.GetPool("Gem", transform.position, transform.rotation);

            initHP = enemyData.hp;
            hp = enemyData.hp;

            StartCoroutine(ReturnToPool());
        }
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(3f);

        PoolManager.Instance.Release(gameObject);
    }
}
