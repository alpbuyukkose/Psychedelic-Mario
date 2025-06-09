using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoomba : MonoBehaviour
{
    [SerializeField] private Sprite headStepped;
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

    private void HandleHeadStepped()
    {
        col.enabled = false;
        movement.enabled = false;
        animator.enabled = false;

        spriteRenderer.sprite = headStepped;
        Destroy(gameObject, .35f);
    }

    private void HandleHit()
    {
        GetComponent<SpriteAnimator>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerState playerState = collision.gameObject.GetComponent<PlayerState>();
            if (PhysicsUtils.DotTest(transform, collision.transform, Vector2.up))
            {
                HandleHeadStepped();
            } else
            {
                playerState.HandleHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            HandleHit();
        }
    }
}
