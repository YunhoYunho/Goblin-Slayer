using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GoblinController : MonoBehaviour
{
    public enum State { Idle, Trace, Attack, Die }

    [Header("General")]
    [SerializeField]
    private State state;
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private float traceDist;
    [SerializeField]
    private float attackDist;
    private float damage;

    private EnemyHealth health;
    private NavMeshAgent agent;
    private Animator anim;
    private Transform enemyTr;
    private Transform playerTr;
    private Coroutine updateRoutine;
    private Coroutine stateRoutine;

    private void Awake()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        health = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        ResetState();
        StartCoroutine(UpdateRoutine());
        StartCoroutine(StateRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateRoutine());
        StopCoroutine(StateRoutine());
    }

    private void ResetState()
    {
        state = State.Idle;
        agent.enabled = true;
        agent.isStopped = false;
        anim.SetBool("IsTrace", false);
        anim.SetBool("IsAttack", false);

        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!health.isDead)
        {
            if (agent.remainingDistance >= 2.0f)
            {
                Vector3 direction = agent.desiredVelocity;

                if (direction.sqrMagnitude >= 0.1f * 0.1f)
                {
                    Quaternion rot = Quaternion.LookRotation(direction);
                    enemyTr.rotation = Quaternion.Slerp(
                        enemyTr.rotation, rot, Time.deltaTime * 10.0f);
                }
            }
        }
    }

    private IEnumerator UpdateRoutine()
    {
        while (!health.isDead)
        {
            yield return new WaitForSeconds(0.3f);

            if (state == State.Die)
                yield break;

            CalculateDist();

            if (health.HP <= 0)
            {
                state = State.Die;
                break;
            }
        }
    }

    private void CalculateDist()
    {
        Vector3 offset = playerTr.position - transform.position;
        float dist = offset.sqrMagnitude;

        if (dist <= attackDist * attackDist)
        {
            state = State.Attack;
        }
        else if (dist <= traceDist * traceDist)
        {
            state = State.Trace;
        }
        else
        {
            state = State.Idle;
        }
    }

    private IEnumerator StateRoutine()
    {
        while (!health.isDead)
        {
            switch (state)
            {
                case State.Idle: IdleState(); break;
                case State.Trace: TraceState(); break;
                case State.Attack: AttackState(); break;
                case State.Die: DieState(); break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void IdleState()
    {
        agent.isStopped = true;
        anim.SetBool("IsTrace", false);
    }

    private void TraceState()
    {
        agent.SetDestination(playerTr.position);
        agent.isStopped = false;
        anim.SetBool("IsTrace", true);
        anim.SetBool("IsAttack", false);
    }

    private void AttackState()
    {
        agent.isStopped = true;
        anim.SetBool("IsAttack", true);
    }

    private void DieState()
    {
        health.Die();

        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

        agent.isStopped = true;
        agent.enabled = false;
        anim.SetBool("IsAttack", false);
    }
}
