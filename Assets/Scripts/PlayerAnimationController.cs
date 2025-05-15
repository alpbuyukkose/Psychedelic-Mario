using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    [Header("Sprites")]
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite jump;
    [SerializeField] private Sprite slide;
    [SerializeField] private SpriteAnimator run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    { 
        run.enabled = playerController.isRunning; // number three priority

        if (playerController.isJumping) // number one priority
        {
            spriteRenderer.sprite = jump;
        }
        else if (playerController.isSliding) // number two priority
        {
            spriteRenderer.sprite = slide;
        }
        else if (!playerController.isRunning) // number four priority
        {
            spriteRenderer.sprite = idle;
        }
    }
}
