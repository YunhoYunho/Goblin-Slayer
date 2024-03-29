using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttacker : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private float damage;

    [Space]

    [Header("AttackGizmos")]
    [SerializeField]
    private bool attackGizmos;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    [Range(0f, 360f)]
    private float attackAngle;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        damage = itemData.abilityValue;
    }

    public void Attack()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        OnAttack();
    }

    public void OnAttack()
    {
        anim.SetTrigger("OnAttack");
    }

    public void OnAttackStart()
    {
        weapon.EnableWeapon();
    }

    public void OnAttackHit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            Vector3 rightDir = AngleUtils.AngleToDir(transform.eulerAngles.y + attackAngle * 0.5f);

            if (Vector3.Dot(transform.forward, dirToTarget) > Vector3.Dot(transform.forward, rightDir))
            {
                if (isPlayer)
                {
                    if (colliders[i].gameObject.CompareTag("Enemy"))
                    {
                        EnemyHealth target = colliders[i].GetComponent<EnemyHealth>();

                        if (target != null)
                        {
                            target.TakeHit(damage);
                        }
                    }
                }
                else
                {
                    if (colliders[i].gameObject.CompareTag("Player"))
                    {
                        PlayerHealth target = colliders[i].GetComponent<PlayerHealth>();

                        if (target != null)
                        {
                            target.TakeHit(damage);
                        }
                    }
                }
            }
        }
    }

    public void OnAttackEnd()
    {
        weapon.DisableWeapon();
    }

    public void OnAttackSound()
    {
        SoundManager.Instance.PlaySFX(SFX.Attack);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Vector3 rightDir = AngleUtils.AngleToDir(transform.eulerAngles.y + attackAngle * 0.5f);
            Vector3 leftDir = AngleUtils.AngleToDir(transform.eulerAngles.y - attackAngle * 0.5f);
            Debug.DrawRay(transform.position, rightDir * attackRange, Color.blue);
            Debug.DrawRay(transform.position, leftDir * attackRange, Color.blue);
        }
    }
}
