using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    public float moveSpeed = 5f; // Adjust this value to control the movement speed.

    private PlayerStats playerStats;
    private Rigidbody2D rb;

    float horizontal;
    float vertical;

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
        horizontal = (Input.GetAxis("Horizontal") * moveSpeed) +(playerStats.GetSpeedBonusPercentage() * (Input.GetAxis("Horizontal") * moveSpeed));
        vertical = (Input.GetAxis("Vertical") * moveSpeed) + (playerStats.GetSpeedBonusPercentage() * (Input.GetAxis("Vertical") * moveSpeed));

        // Combine input into a movement vector

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        rb.linearVelocity = ((movement * moveSpeed) + (playerStats.GetSpeedBonusPercentage() * (movement * moveSpeed)));
    }
}