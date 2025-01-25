using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections;

public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PickUpRange"))
        {

            Sequence boopSequence = DOTween.Sequence();

            // Jump effect (boop)
            boopSequence.Append(transform.DOLocalMoveY(1.0f, 0.25f).SetRelative().SetEase(Ease.OutQuad))
                        .OnComplete(() => MoveToTarget(col.transform));
        }
    }
    void MoveToTarget(Transform col)
    {
        // Move towards the target using Unity's built-in function
        StartCoroutine(MoveTowards(col));
    }
    IEnumerator MoveTowards(Transform col)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = col.position;
        float elapsedTime = 0f;

        while (elapsedTime < 0.25f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 0.25f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact target position
        transform.position = targetPosition;

        // Call FunctionX or any other function after reaching the target
        PickUp();
    }
    void PickUp()
    {
        Collect();
        Destroy(gameObject);
    }
    public abstract void Collect();
}
