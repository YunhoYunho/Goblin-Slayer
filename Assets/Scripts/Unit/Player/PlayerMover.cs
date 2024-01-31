using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpSpeed;

    [Header("InterAction")]
    [SerializeField]
    private bool interActionGizmos;
    [SerializeField]
    private float interActionRange;
    [SerializeReference, Range(0f, 360f)]
    private float interActionAngle;

    private SwordAttacker attacker;
    private Animator anim;
    private CharacterController controller;
    private float moveY;
    private bool isMove;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        attacker = GetComponent<SwordAttacker>();
    }

    private void Update()
    {
        if (!GameManager.Instance.isMobile)
        {
            Move();
            Rotate();
        }
        InterAction();
        attacker.Attack();
    }

    private void Move()
    {
        Vector3 moveInput = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        if (moveInput.sqrMagnitude > 1f) moveInput.Normalize();
        isMove = moveInput.magnitude != 0;
        anim.SetBool("IsMove", isMove);

        if (isMove)
        {
            Vector3 forwardVec = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
            Vector3 rightVec = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;
            Vector3 moveVec = forwardVec * moveInput.z + rightVec * moveInput.x;
            controller.Move(moveVec * moveSpeed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(moveVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.2f);
        }
    }

    private void Rotate()
    {
        transform.forward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
    }

    public void Move(Vector2 inputDir)
    {
        Vector2 moveInput = inputDir;
        isMove = moveInput.magnitude != 0;
        anim.SetBool("IsMove", isMove);

        if (isMove)
        {
            Vector3 forwardVec = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
            Vector3 rightVec = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

            Vector3 moveVec = forwardVec * moveInput.y + rightVec * moveInput.x;
            controller.Move(moveVec * moveSpeed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(moveVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.2f);
        }
    }

    private void InterAction()
    {
        if (!Input.GetButtonDown("InterAction"))
            return;

        Collider[] colliders = Physics.OverlapSphere(
            transform.position, interActionRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToTarget =
                (colliders[i].transform.position - transform.position).normalized;
            Vector3 rightDir = AngleToDir(transform.eulerAngles.y + interActionAngle * 0.5f);

            if (Vector3.Dot(transform.forward, dirToTarget) >
                Vector3.Dot(transform.forward, rightDir))
            {
                IInteractable target = colliders[i].GetComponent<IInteractable>();
                target?.InterAction();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interActionGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, interActionRange);

            Vector3 rightDir = AngleToDir(transform.eulerAngles.y + interActionAngle * 0.5f);
            Vector3 leftDir = AngleToDir(transform.eulerAngles.y - interActionAngle * 0.5f);
            Debug.DrawRay(transform.position, rightDir * interActionRange, Color.blue);
            Debug.DrawRay(transform.position, leftDir * interActionRange, Color.blue);
        }
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
