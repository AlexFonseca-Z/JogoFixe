using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Side to Side Movement
        float moveInput = 0f;
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            moveInput = 1f;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //Jumping
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded || Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        //Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if(isGrounded)
        {
            if(moveInput != 0)
            {
                animator.Play("Player_Run");
            }
            else
            {
                animator.Play("Player_Idle");
            }
        }
        else
        {
            if(rb.linearVelocity.y > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }
        }
        
    }
}