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
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
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
        if(context.performed)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}
