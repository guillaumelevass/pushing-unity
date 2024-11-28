using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Reference to the enemy prefab
    public GameObject enemyPrefab;

    // Number of enemies to spawn
    public int numberOfEnemies = 10;

    // Reference to the Hero GameObject
    public GameObject hero;

    // Radius within which enemies will be spawned around the Hero
    public float spawnRadius = 10f;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        if (hero == null)
        {
            Debug.LogError("Hero GameObject is not assigned!");
            return;
        }

        Vector3 heroPosition = hero.transform.position;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generate a random position around the hero within the specified radius
            Vector3 spawnPosition = GetRandomPositionAroundHero(heroPosition, spawnRadius);

            // Instantiate the enemy at the generated position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionAroundHero(Vector3 center, float radius)
    {
        // Generate a random direction
        Vector2 randomDirection = Random.insideUnitCircle * radius;

        // Create the spawn position based on the random direction
        Vector3 spawnPosition = new Vector3(center.x + randomDirection.x, 0, center.z + randomDirection.y);
        
        return spawnPosition;
    }
}