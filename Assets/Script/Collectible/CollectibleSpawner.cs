using UnityEngine;
using System.Collections.Generic;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public float initialSpawnInterval = 5f;
    public float minSpawnInterval = 1f;
    public float spawnSpeedIncreaseRate = 0.95f;

    public Vector2 spawnAreaMin; // Bottom-left of spawn area
    public Vector2 spawnAreaMax; // Top-right of spawn area

    public int maxCollectibles = 10;
    private List<GameObject> activeCollectibles = new List<GameObject>();

    public LayerMask obstacleLayer; 
    public float spawnRadius = 0.5f; 

    private float currentSpawnInterval;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating(nameof(SpawnCollectible), 2f, currentSpawnInterval);
    }

    private void SpawnCollectible()
    {
        if (collectiblePrefabs.Length == 0 || activeCollectibles.Count >= maxCollectibles) return;

        GameObject collectibleToSpawn = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];

        Vector2 spawnPosition;
        int attempts = 10;

        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);

            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, obstacleLayer))
            {
                break; 
            }

            attempts--;
        }
        while (attempts > 0);

        if (attempts > 0) 
        {
            GameObject spawnedCollectible = Instantiate(collectibleToSpawn, spawnPosition, Quaternion.identity);
            activeCollectibles.Add(spawnedCollectible);

            StartCoroutine(DespawnCollectible(spawnedCollectible, 10f));

            IncreaseSpawnRate();
        }
    }

    private void IncreaseSpawnRate()
    {
        CancelInvoke(nameof(SpawnCollectible));
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval * spawnSpeedIncreaseRate);
        InvokeRepeating(nameof(SpawnCollectible), currentSpawnInterval, currentSpawnInterval);
    }

    private System.Collections.IEnumerator DespawnCollectible(GameObject collectible, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        if (collectible != null)
        {
            activeCollectibles.Remove(collectible);
            Destroy(collectible);
        }
    }
}
