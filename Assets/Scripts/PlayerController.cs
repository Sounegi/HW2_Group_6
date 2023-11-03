using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    [Header("Speed Values")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpSpeed = 3f;

    private Vector3 move;
    private Vector2 movementInput;
    private Vector2 jumpInput;
    private bool onGround;

    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckOnGround();

        if(movementInput == Vector2.zero)
        {
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Move", true);
        }

        anim.SetFloat("X", movementInput.x);
        anim.SetFloat("Z", movementInput.y);

        anim.SetBool("OnGround", onGround);

        move = new Vector3(movementInput.x, 0, movementInput.y);
        move = move * moveSpeed + Vector3.up * rb.velocity.y;
        rb.velocity = move;
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
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    private void CheckOnGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
        {
            onGround = false;
        }
        else
        {
            onGround = true;
        }
    }
}
