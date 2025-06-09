using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 2.5f;
    public Vector2 moveDir = Vector2.left;

    private Vector2 velocity;
    private float gravity = -9.81f;

    private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void FixedUpdate()
    {
        velocity.x = moveDir.x * moveSpeed;

        if (!isGrounded)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
        else
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rb.WakeUp();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.75f)
            {
                isGrounded = true;
            }
            else if (Mathf.Abs(contact.normal.x) > 0.75f)
            {
                if (!collision.gameObject.CompareTag("Player"))
                {
                    moveDir.x *= -1;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isGrounded && PhysicsUtils.DotTest(transform, collision.transform, Vector2.up, 0.75f))
        {
            isGrounded = false;
        }
    }
}


