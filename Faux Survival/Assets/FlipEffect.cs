using UnityEngine;

public class FlipEffect : MonoBehaviour
{
    public SpriteRenderer sr;

    private void Update()
    {
        Transform player = FindFirstObjectByType<PlayerMovement>().transform;
        if(player.position.x>transform.position.x)
            sr.flipX = true;
    }
}
