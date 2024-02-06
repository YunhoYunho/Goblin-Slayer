using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject target;
    public float range;
    public float angle;
    [SerializeField]
    private bool isFovGizmos;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask obstableMask;

    private void Update()
    {
        FindTarget();
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (Physics.Raycast(transform.position, dirToTarget, distToTarget, obstableMask))
                continue;

            target = colliders[i].gameObject;
            return;
        }
        target = null;
    }

    public void OnDrawGizmosSelected()
    {
        if (isFovGizmos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, range);

            Vector3 lookDir = AngleUtils.AngleToDir(transform.eulerAngles.y);
            Vector3 rightDir = AngleUtils.AngleToDir(transform.eulerAngles.y + angle * 0.5f);
            Vector3 leftDir = AngleUtils.AngleToDir(transform.eulerAngles.y - angle * 0.5f);

            Debug.DrawRay(transform.position, lookDir * range, Color.green);
            Debug.DrawRay(transform.position, rightDir * range, Color.blue);
            Debug.DrawRay(transform.position, leftDir * range, Color.blue);
        }
    }
}
