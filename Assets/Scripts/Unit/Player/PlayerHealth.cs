using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UnitHealth
{
    [SerializeField]
    private PlayerData playerData;
    public bool isInvincible;

    [HideInInspector]
    public Animator anim;
    private Collider coll;
    private PlayerMover mover;
    private SwordAttacker attacker;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
        mover = GetComponent<PlayerMover>();
        attacker = GetComponent<SwordAttacker>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        initHP = playerData.hp;
        hp = initHP;
        mover.enabled = true;
        attacker.enabled = true;
        isInvincible = false;
    }

    public override void TakeHit(float damage)
    {
        if (isDead || isInvincible)
            return;

        base.TakeHit(damage);
    }

    public void RestoreHP(float amount, float during)
    {
        if (isDead)
            return;

        if (HP <= initHP)
        {
            StartCoroutine(RestoreRoutine(amount, during));
        }
    }

    private IEnumerator RestoreRoutine(float amount, float during)
    {
        float curTime = 0;

        while (curTime < during)
        {
            HP += amount * Time.deltaTime;

            if (HP >= initHP)
            {
                HP = initHP;
                yield break;
            }
            curTime += Time.deltaTime;
            yield return null;
        }
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("OnDeath");
        coll.enabled = false;
        mover.enabled = false;
        attacker.enabled = false;
    }
}
