using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private float deathAnimDuration = 2.5f;
    [SerializeField] private float gravity = -36f;
    [SerializeField] private float deathJumpMag = 9f;
    private float count;

    private void OnEnable()
    {
        sr.sortingLayerName = "DeathLayer";
        sr.sprite = deathSprite;

        GetComponent<Rigidbody2D>().isKinematic = true;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null )
        {
            playerController.enabled = false;
        }

        GenericMovement genericMovement = GetComponent<GenericMovement>();
        if ( genericMovement != null )
        {
            genericMovement.enabled = false;
        }

        StartCoroutine(PlayDeathAnim());
    }

    private IEnumerator PlayDeathAnim()
    {
        sr.enabled = true;
        count = 0;

        Vector2 deathVel = Vector2.up * deathJumpMag;
        Vector2 currentPos = transform.position;

        while (count < deathAnimDuration)
        {
            count += Time.deltaTime;
            deathVel += Vector2.up * gravity * Time.deltaTime;

            currentPos += deathVel * Time.deltaTime;
            transform.position = currentPos;

            yield return null;
        }

        //Destroy(gameObject);
    }

}
