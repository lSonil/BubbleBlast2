using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 50; // Initial health of the enemy.

    private int currentHealth;

    private void Start()
    {
        // Initialize the enemy's current health.
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy is hit by a player's Weapon.
        if (other.CompareTag("PlayerWeapon"))
        {
            // Reduce the enemy's health by 20.
            TakeDamage(20);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        // Reduce the enemy's health by the damage amount.
        currentHealth -= damageAmount;

        // Check if the enemy's health has reached zero or less.
        if (currentHealth <= 0)
        {
            // Destroy the enemy GameObject.
            Destroy(gameObject);
        }
    }
}
