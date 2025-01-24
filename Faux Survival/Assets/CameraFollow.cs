using UnityEngine;

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

            //targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
            //targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
