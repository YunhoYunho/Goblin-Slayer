using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    private PlayerAttacker attacker;
    private Animator anim;
    private CharacterController controller;
    private float moveY;

    [Header("General")]
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

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        attacker = GetComponent<PlayerAttacker>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Rotate();
        Jump();
        InterAction();
        attacker.Attack();
    }

    private void Move()
    {
        Vector3 fowardVec = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;

        Vector3 moveInput = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        if (moveInput.sqrMagnitude > 1f) moveInput.Normalize();

        anim.SetFloat("XInput", Input.GetAxis("Horizontal"));
        anim.SetFloat("YInput", Input.GetAxis("Vertical"));

        Vector3 moveVec = fowardVec * moveInput.z + rightVec * moveInput.x;

        controller.Move(moveVec * moveSpeed * Time.deltaTime);

        anim.SetFloat("MoveSpeed", moveSpeed);
    }

    private void Rotate()
    {
        transform.forward = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
    }

    private void Jump()
    {
        moveY += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            moveY = jumpSpeed;
        }
        else if (controller.isGrounded && moveY < 0)
        {
            moveY = 0;
        }

        controller.Move(Vector3.up * moveY * Time.deltaTime);
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
