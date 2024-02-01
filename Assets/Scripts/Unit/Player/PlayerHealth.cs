using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UnitHealth
{
    [SerializeField]
    private PlayerData playerData;
    public bool isInvincible;

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
        isInvincible = false;
    }

    public override void TakeHit(int damage)
    {
        if (isDead || isInvincible)
            return;

        base.TakeHit(damage);
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
