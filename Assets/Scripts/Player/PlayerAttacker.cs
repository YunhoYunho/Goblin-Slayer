using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private bool attackGizmos;
    [SerializeField]
    private float attackRange;
    [SerializeReference, Range(0f, 360f)]
    private float attackAngle;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        anim.SetTrigger("OnAttack");
    }

    public void OnAttackStart()
    {
        weapon.EnableWeapon();
    }

    public void OnAttackHit()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, attackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToTarget =
                (colliders[i].transform.position - transform.position).normalized;
            Vector3 rightDir = AngleToDir(transform.eulerAngles.y + attackAngle * 0.5f);

            if (Vector3.Dot(transform.forward, dirToTarget) >
                Vector3.Dot(transform.forward, rightDir))
            {
                if (colliders[i].gameObject.name == "Cube")
                    Destroy(colliders[i].gameObject);
            }
        }
    }

    public void OnAttackEnd()
    {
        weapon.DisableWeapon();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Vector3 rightDir = AngleToDir(transform.eulerAngles.y + attackAngle * 0.5f);
            Vector3 leftDir = AngleToDir(transform.eulerAngles.y - attackAngle * 0.5f);
            Debug.DrawRay(transform.position, rightDir * attackRange, Color.blue);
            Debug.DrawRay(transform.position, leftDir * attackRange, Color.blue);
        }
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
