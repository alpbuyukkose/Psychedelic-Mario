using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Control")]
    [SerializeField] private float moveSpeed = 9f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float gravity = 38f;
    [SerializeField] private float jumpMultiplier = 3f;
    [SerializeField] private float fallMultiplier = 1.5f;
    private bool jumpPressed;
    private bool jumpHeld;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckRadius = .15f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    // Animation Control 
    // These properties are only readable from outside the class,
    // their values cannot be changed externally (encapsulation).
    public bool isJumping { get; private set; }
    public bool isIdle => Mathf.Abs(inputDir) < 0.01f && Mathf.Abs(velocity.x) < 0.01f;
    public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputDir) > 0.25f;
    public bool isSliding => (inputDir > 0f && velocity.x < 0f) || (inputDir < 0f && velocity.x > 0f);

    // Input Control
    private float inputDir;
    private Vector2 velocity;
    private Vector2 nextPosition;

    private Rigidbody2D rb;
    [SerializeField] private Camera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovementInput();
        JumpInput();
    }

    private void MovementInput()
    {
        inputDir = Input.GetAxisRaw("Horizontal");
        // Accelerated Motion
        velocity.x = Mathf.MoveTowards(velocity.x, inputDir * moveSpeed, moveSpeed * Time.deltaTime);

        // Player rotation to faced direction
        if (velocity.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (velocity.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        jumpHeld = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);

        HorizontalMovement();
        Jump();
    }

    private void HorizontalMovement()
    {
        // Movement
        nextPosition = rb.position + velocity * Time.fixedDeltaTime;

        // Edge Clamping
        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, 0f));
        nextPosition.x = Mathf.Clamp(nextPosition.x, leftEdge.x + .5f, rightEdge.x - .5f);

        rb.MovePosition(nextPosition);
    }

    private void Jump()
    {
        if (isGrounded && jumpPressed)
        {
            velocity.y = jumpForce;
            jumpPressed = false;
            isJumping = true;
        }

        if (!isGrounded)
        {
            if (velocity.y > 0 && !jumpHeld) // While going up if player ends jump early, increase the gravity and jump will be short.
            {
                velocity.y -= gravity * jumpMultiplier * Time.fixedDeltaTime;
            }
            else if (velocity.y < 0) // While going down, increase the gravity and fasten the fall.
            {
                velocity.y -= gravity * fallMultiplier * Time.fixedDeltaTime;
            }
            else // Othercases like, pressing jump button and while going up, gravity stays same and jump will be higher.
            {
                velocity.y -= gravity * 1f * Time.fixedDeltaTime;
            }
            isJumping = true;
        }
        else if (isGrounded && velocity.y < 0) // When landed in ground and velocity of y is negative, reset the velocity of y axis.
        {
            velocity.y = 0;
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (PhysicsUtils.DotTest(transform, collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }

            if (PhysicsUtils.DotTest(transform, collision.transform, Vector2.right, .8f))
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            if (PhysicsUtils.DotTest(transform, collision.transform, Vector2.left, .8f))
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}