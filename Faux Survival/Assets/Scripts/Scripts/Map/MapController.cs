using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement playerMovement;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    private Vector3 noTerrainPosition;
    GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    // Data structure to store generated chunk positions
    HashSet<Vector3> generatedChunkPositions = new HashSet<Vector3>();

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimzer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        Vector3[] adjacentPositions = new Vector3[8];
        adjacentPositions[0] = currentChunk.transform.Find("Right").position;
        adjacentPositions[1] = currentChunk.transform.Find("Right Up").position;
        adjacentPositions[2] = currentChunk.transform.Find("Right Down").position;
        adjacentPositions[3] = currentChunk.transform.Find("Left").position;
        adjacentPositions[4] = currentChunk.transform.Find("Left Up").position;
        adjacentPositions[5] = currentChunk.transform.Find("Left Down").position;
        adjacentPositions[6] = currentChunk.transform.Find("Up").position;
        adjacentPositions[7] = currentChunk.transform.Find("Down").position;

        for (int i = 0; i < adjacentPositions.Length; i++)
        {
            // Check if a chunk has already been generated at this position
            if (!generatedChunkPositions.Contains(adjacentPositions[i]) &&
                !Physics2D.OverlapCircle(adjacentPositions[i], checkerRadius, terrainMask))
            {
                noTerrainPosition = adjacentPositions[i];
                SpawnChunk();
                generatedChunkPositions.Add(adjacentPositions[i]); // Add the position to the set of generated positions
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        latestChunk.transform.SetParent(transform, false);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimzer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;   //Check every 1 second to save cost, change this value to lower to check more times
        }
        else
        {
            return;
        }

        for (int i = spawnedChunks.Count - 1; i >= 0; i--)
        {
            GameObject chunk = spawnedChunks[i];
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);

            if (opDist > maxOpDist)
            {
                generatedChunkPositions.Remove(chunk.transform.position);
                spawnedChunks.RemoveAt(i);
                Destroy(chunk);
            }
        }

    }
}
