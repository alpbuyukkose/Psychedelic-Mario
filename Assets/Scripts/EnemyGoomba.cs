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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PhysicsUtils.DotTest(transform, collision.transform, Vector2.up))
            {
                HandleHeadStepped();
            }
        }
    }
}
