using UnityEngine;
using DG.Tweening;

public class BoopEffect : MonoBehaviour
{
    private Transform spriteTransform;
    private Rigidbody2D rb;

    public float maxJumpHeight = 1.0f;
    public float minJumpHeight = 0.5f;
    public float maxDuration = 0.5f;
    public float minDuration = 0.25f;

    void Start()
    {
        spriteTransform = GetComponentInParent<Transform>(); ;
        rb = GetComponentInParent<Rigidbody2D>();

        // Start the boop effect
        Boop();
    }

    void Boop()
    {
        // Determine the current height and duration based on player movement
        float currentHeight = rb.linearVelocity.magnitude > 0 ? minJumpHeight : maxJumpHeight;
        float currentDuration = rb.linearVelocity.magnitude > 0 ? minDuration : maxDuration;

        // Sequence for the boop effect
        Sequence mySequence = DOTween.Sequence();

        // Jump effect
        mySequence.Append(spriteTransform.DOLocalMoveY(currentHeight, currentDuration).SetRelative().SetEase(Ease.OutQuad))
                  .Join(spriteTransform.DOScale(new Vector3(0.9f, 1.1f, 1.0f), currentDuration).SetEase(Ease.InQuad))
                  .Append(spriteTransform.DOLocalMoveY(-currentHeight, currentDuration).SetRelative().SetEase(Ease.InQuad))
                  .Join(spriteTransform.DOScale(new Vector3(1.1f, 0.9f, 1.0f), currentDuration).SetEase(Ease.InQuad));

        // Stretch wider when going down and longer when going up

        // Make the sequence call Boop again when done
        mySequence.OnComplete(Boop);

        // Play the sequence
        mySequence.Play();
    }
}
