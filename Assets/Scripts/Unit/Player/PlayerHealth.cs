using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UnitHealth
{
    [SerializeField]
    private PlayerData playerData;

    private Collider coll;
    private PlayerMover mover;
    private SwordAttacker attacker;
    private Animator anim;

    private void Awake()
    {
        mover = GetComponent<PlayerMover>();
        attacker = GetComponent<SwordAttacker>();
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        initHP = playerData.hp;
        hp = initHP;
        mover.enabled = true;
        attacker.enabled = true;
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);

        if (isDead)
            return;

        anim.SetTrigger("OnTakeHit");
    }

    public override void RestoreHP(int amount)
    {
        base.RestoreHP(amount);
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
