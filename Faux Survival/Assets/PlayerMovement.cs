using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float moveSpeed = 5f; // Adjust this value to control the movement speed.

    private PlayerStats playerStats;
    private Rigidbody2D rb;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Call the movement function in Update so that movement is smooth and responsive.
        Move();
    }

    private void Move()
    {
        if ((movementJoystick.GetJoystickVect().y != 0) || (movementJoystick.GetJoystickVect().x != 0))
        {
            rb.linearVelocity = new Vector2((movementJoystick.GetJoystickVect().x * moveSpeed) + (playerStats.GetSpeedBonusPercentage() * (movementJoystick.GetJoystickVect().x * moveSpeed)), (movementJoystick.GetJoystickVect().y * moveSpeed) + (playerStats.GetSpeedBonusPercentage() * (movementJoystick.GetJoystickVect().y * moveSpeed)));
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
