using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameRate = .2f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating("Animate", frameRate, frameRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        currentFrame++;
        if (currentFrame >= frames.Length)
            currentFrame = 0;

        if (currentFrame >= 0 && frames.Length > currentFrame)
            spriteRenderer.sprite = frames[currentFrame];
    }
}
