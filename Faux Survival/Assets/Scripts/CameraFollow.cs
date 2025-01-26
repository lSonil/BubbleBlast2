using UnityEngine;
using DG.Tweening; // Import DOTween namespace

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform that the camera should follow.
    public float smoothSpeed = 0.125f; // A value to control how smoothly the camera follows the player.

    private Vector3 offset;
    public Vector2 maxPos;
    public Vector2 minPos;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = target.position + offset;

            // Uncomment to restrict camera movement to within min and max bounds
            // targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
            // targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }

    // Method to shake the camera lightly
    public void LightShake(float duration = .1f, float strength = .2f, int vibrato = 10, float randomness = 90f)
    {
        // Perform a light shake on the camera's position
        transform.DOShakePosition(duration, strength, vibrato, randomness, fadeOut: true);
    }
}
