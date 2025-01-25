using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner_ORIG : MonoBehaviour
{
    public List<GameObject> EnemyTypes; // List of enemy prefabs to spawn.
    public float spawnInterval = 3.0f; // Interval between enemy spawns.
    public float spawnRadius = 5.0f; // Maximum distance from the player to spawn enemies.
    public float minDistanceBetweenEnemies = 2.0f; // Minimum distance between spawned enemies.
    [SerializeField] private GameObject Player;

    private float nextSpawnTime;

    private void Start()
    {
        // Initialize the first spawn time.
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        // Check if it's time to spawn a new enemy.
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            // Update the next spawn time.
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (EnemyTypes.Count == 0)
        {
            Debug.LogWarning("No enemy types in the list.");
            return;
        }

        // Randomly select an enemy type from the list.
        int randomIndex = Random.Range(0, EnemyTypes.Count);
        GameObject enemyPrefab = EnemyTypes[randomIndex];

        // Calculate a random offscreen position relative to the player.
        Vector2 spawnPosition = GetRandomOffscreenPosition();

        // Check if the spawn position is too close to existing enemies.
        if (IsTooCloseToOtherEnemies(spawnPosition))
        {
            return; // Skip spawning if too close.
        }

        // Instantiate the enemy at the calculated position.
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomOffscreenPosition()
    {
        print(2);
        // Calculate a random angle and distance.
        float angle = Random.Range(0f, Mathf.PI * 2); // Random angle in radians.
        float distance = Random.Range(spawnRadius / 2, spawnRadius);

        // Calculate the random position relative to the player.
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        Vector2 spawnPosition = (Vector2)Player.transform.position + offset;

        return spawnPosition;
    }

    private bool IsTooCloseToOtherEnemies(Vector2 spawnPosition)
    {
        // Check if the new spawn position is too close to existing enemies.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, minDistanceBetweenEnemies);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return true;
            }
        }

        return false;
    }
}
