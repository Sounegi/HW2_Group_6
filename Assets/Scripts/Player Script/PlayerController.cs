using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    private Rigidbody rb;
    private Animator anim;

    [Header("Speed Values")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpSpeed = 3f;

    private Vector3 move;
    private Vector2 movementInput;
    private Vector2 mouseInput;
    private bool onGround;
    private bool attacking;

    public bool isAttacking;
    public LayerMask groundLayer;

    void Awake()
    {
        instance = this;
    }

    public static PlayerController GetInstance()
    {
        return instance;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("Move", movementInput != Vector2.zero);

        anim.SetFloat("X", movementInput.x);
        anim.SetFloat("Z", movementInput.y);

        anim.SetBool("OnGround", onGround);

        anim.SetBool("Attacking", attacking);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack"))
        {
            AxeBehaviour.GetInstance().Attack();
            rb.velocity = new Vector3 (0, 0, 0);
            isAttacking = true;
            return;
        }
        else
        {
            AxeBehaviour.GetInstance().Reset();
            isAttacking = false;
        }
        
        Vector3 forwardDirection = transform.forward;
        Vector3 adjustedMove = forwardDirection * movementInput.y + transform.right * movementInput.x;
        move = new Vector3(adjustedMove.x, 0, adjustedMove.z) * moveSpeed + Vector3.up * rb.velocity.y;

        rb.velocity = move;
        rb.angularVelocity = new Vector3(0, mouseInput.x, 0);
        MainCamera.GetInstance().RotateCamera(mouseInput.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
            movementInput.Normalize();
        }
        if(context.canceled)
        {
            movementInput = context.ReadValue<Vector2>();
            movementInput.Normalize();
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && onGround)
        {
            StartCoroutine(Jump());
            // rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            onGround = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed && onGround && !isAttacking)
        {
            attacking = true;
        }
        if(context.canceled)
        {
            attacking = false;
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            mouseInput = context.ReadValue<Vector2>();
        }
        if(context.canceled)
        {
            mouseInput = context.ReadValue<Vector2>();
        }
    }

    public void ChangeGroundState(bool val)
    {
        onGround = val;
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.5f);

        rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
    }
}
