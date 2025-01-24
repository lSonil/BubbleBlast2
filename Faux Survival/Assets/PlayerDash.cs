using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 10f; // The speed of the dash.
    public float dashDuration = 0.2f; // How long the dash lasts.
    public float dashCooldown = 1f; // Cooldown time between dashes.

    private Rigidbody2D rb;
    private Vector2 dashDirection = Vector2.zero;
    private bool canDash = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Dash()
    {
        Vector2 direction = GetComponent<PlayerMovement>().movementJoystick.GetJoystickVect();

        if (canDash && direction != Vector2.zero)
        {
            Debug.Log("Dashing!");

            // Set the dash direction and start the cooldown.
            dashDirection = direction.normalized;
            canDash = false;

            // Start the dash coroutine.
            StartCoroutine(PerformDash());

            // Start the cooldown coroutine.
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator PerformDash()
    {
        // Apply a velocity to the player to perform the dash.
        rb.linearVelocity = dashDirection * dashSpeed;

        // Wait for the dash duration.
        yield return new WaitForSeconds(dashDuration);

        // Check if the player can still dash (cooldown hasn't started).
        if (canDash)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator StartCooldown()
    {
        // Wait for the dash cooldown.
        yield return new WaitForSeconds(dashCooldown);

        // Reset the canDash flag.
        canDash = true;
    }
}
