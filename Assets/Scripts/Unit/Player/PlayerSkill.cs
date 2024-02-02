using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("AttackSkill")]
    [SerializeField]
    private ParticleSystem jumpAttackEffect;
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool skillAttackGizmos;
    [SerializeField]
    private float skillRange;
    [SerializeField]
    private float skillAngle;

    [Space]

    [Header("BuffSkill")]
    [SerializeField]
    private ParticleSystem healBuffEffect;
    [SerializeField]
    private ParticleSystem duringEffect;
    [SerializeField]
    private float duration;

    private PlayerHealth health;
    private Animator anim;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ActivateSkill(SkillData skillData)
    {
        anim.SetTrigger(skillData.animationName);
        anim.SetLayerWeight(1, 0);
    }

    public void OnAttackSkill()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, skillRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            Vector3 rightDir = AngleUtils.AngleToDir(transform.eulerAngles.y + skillAngle * 0.5f);

            if (Vector3.Dot(transform.forward, dirToTarget) > Vector3.Dot(transform.forward, rightDir))
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
        }
        StartCoroutine(EffectRoutine(jumpAttackEffect));
    }

    public void OnBuffSkill()
    {
        StartCoroutine(UnDamageableRoutine());
        StartCoroutine(EffectRoutine(healBuffEffect));
    }

    private IEnumerator UnDamageableRoutine()
    {
        health.isInvincible = true;
        duringEffect.Play();
        yield return new WaitForSeconds(duration);
        health.isInvincible = false;
        duringEffect.Stop();
    }

    private IEnumerator EffectRoutine(ParticleSystem particleSystem)
    {
        particleSystem.Play();
        yield return new WaitForSeconds(1.9f);
        anim.SetLayerWeight(1, 1);
        particleSystem.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        if (skillAttackGizmos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, skillRange);

            Vector3 rightDir = AngleUtils.AngleToDir(transform.eulerAngles.y + skillAngle * 0.5f);
            Vector3 leftDir = AngleUtils.AngleToDir(transform.eulerAngles.y - skillAngle * 0.5f);
            Debug.DrawRay(transform.position, rightDir * skillRange, Color.cyan);
            Debug.DrawRay(transform.position, leftDir * skillRange, Color.cyan);
        }
    }
}
