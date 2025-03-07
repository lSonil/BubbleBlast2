using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;
        public float spawnInterval;
        public int spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints;

    bool isWaveStarting = false;
    Transform player;
    List<GameObject> activeEnemies = new List<GameObject>();
    AudioSource sound;

    void Start()
    {
        player = FindFirstObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
        sound = GetComponent<AudioSource>();

    }

    void Update()
    {
        // Check if all waves have been spawned and all enemies are gone
        if (currentWaveCount >= waves.Count - 1 && waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota && enemiesAlive == 0)
        {
            StartCoroutine(RestartWaves());
        }
        // Start next wave when all enemies of the current wave are spawned and max limit isn't reached
        else if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota && enemiesAlive < maxEnemiesAllowed && !isWaveStarting)
        {
            Debug.Log("Starting next wave...");
            isWaveStarting = true; // Prevent multiple calls
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        // Only spawn if the max enemy limit is not reached
        if (spawnTimer >= waves[currentWaveCount].spawnInterval && enemiesAlive < maxEnemiesAllowed)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator RestartWaves()
    {
        yield return new WaitForSeconds(waveInterval); // Wait before restarting

        currentWaveCount = 0; // Reset to first wave

        // Reset spawn counts for all waves
        foreach (var wave in waves)
        {
            wave.spawnCount = 0;
            foreach (var enemyGroup in wave.enemyGroups)
            {
                enemyGroup.spawnCount = 0;
            }
        }

        CalculateWaveQuota(); // Recalculate for the first wave
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            Debug.Log(currentWaveCount);
            CalculateWaveQuota();
        }
        isWaveStarting = false; 
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    GameObject spawnedEnemy = Instantiate(
                        enemyGroup.enemyPrefab,
                        player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,
                        Quaternion.identity
                    );
                    spawnedEnemy.transform.SetParent(transform, false);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    // Add the spawned enemies to the list of active enemies
                    activeEnemies.Add(spawnedEnemy);
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled(GameObject enemy)
    {
        sound.Play();

        // Remove the killed enemy from the list of active enemies
        activeEnemies.Remove(enemy);

        enemiesAlive--;
    }

    public Transform FindClosestEnemy(Vector3 playerPosition)
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in activeEnemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null)
            return closestEnemy.transform;

        return null;
    }

    public Transform FindRandomEnemy(Vector3 playerPosition)
    {
        GameObject closestEnemy = null;

        if(activeEnemies.Count>0)
            closestEnemy = activeEnemies[Random.Range(0, activeEnemies.Count)];

        if (closestEnemy != null)
            return closestEnemy.transform;

        return null;
    }
}
