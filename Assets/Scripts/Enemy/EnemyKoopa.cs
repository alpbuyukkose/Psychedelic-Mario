using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKoopa : MonoBehaviour
{
    [SerializeField] private Sprite shellSprite;
    [SerializeField] private float shellSpeed = 9f;
    private bool isGetHitted;
    private bool isPushed;


    private Collider2D col;
    private GenericMovement movement;
    private SpriteAnimator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        movement = GetComponent<GenericMovement>();
        animator = GetComponent<SpriteAnimator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void HandleShell()
    {
        isGetHitted = true;

        movement.enabled = false;
        animator.enabled = false;

        spriteRenderer.sprite = shellSprite;
        //Destroy(gameObject, .35f);
    }

    private void HandlePushShell(Vector2 direction)
    {
        isPushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;

        GenericMovement movement = GetComponent<GenericMovement>();
        movement.moveDir = direction.normalized;
        movement.moveSpeed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void HandleHit()
    {
        GetComponent<SpriteAnimator>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGetHitted && collision.gameObject.CompareTag("Player"))
        {
            PlayerState playerState = collision.gameObject.GetComponent<PlayerState>();
            if (PhysicsUtils.DotTest(transform, collision.transform, Vector2.up))
            {
                HandleShell();
            }
            else
            {
                playerState.HandleHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGetHitted && collision.gameObject.CompareTag("Player"))
        {
            if (!isPushed)
            {
                Vector2 dir = new Vector2 (transform.position.x - collision.transform.position.x, 0f);
                HandlePushShell(dir);
            } else
            {
                PlayerState playerState = collision.gameObject.GetComponent<PlayerState>();
                playerState.HandleHit();
            }
        } else if (!isGetHitted && collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            HandleHit();
        }
    }

    private void OnBecameInvisible()
    {
        if (isPushed)
        {
            Destroy(gameObject);
        }
    }
}
