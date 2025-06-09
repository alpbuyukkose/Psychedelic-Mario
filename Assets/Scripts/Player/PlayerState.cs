using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController bigPlayerAnimationController;
    [SerializeField] private PlayerAnimationController smallPlayerAnimationController;
    private DeathAnimation deathAnimation;

    private bool IsDead => deathAnimation.enabled;
    private bool isSmall => smallPlayerAnimationController.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void HandleHit()
    {
        if (!isSmall)
        {
            ActivateSmallSprites();
        } else
        {
            HandleDeath();
        }
    }

    private void ActivateSmallSprites()
    {

    }

    private void HandleDeath()
    {
        smallPlayerAnimationController.enabled = false;
        bigPlayerAnimationController.enabled = false;
        deathAnimation.enabled = true;

        StartCoroutine(DelayedResetLevel());
    }

    private IEnumerator DelayedResetLevel()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.ResetLevel();
    }

}
