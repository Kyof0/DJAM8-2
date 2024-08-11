using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public Transform[] spawnPoints; // Array of spawn points for new enemies
    public int numberOfNewEnemies = 3; // Number of enemies to spawn
    private bool hasSpawned = false; // Flag to ensure enemies are only spawned once

    void Update()
    {
        if (!hasSpawned && IsOnlyOneEnemyLeft())
        {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    private bool IsOnlyOneEnemyLeft()
    {
        // Find all objects of type Enemy in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        // Check if there is exactly one enemy left
        return enemies.Length == 1;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfNewEnemies; i++)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            // Instantiate a new enemy at the chosen spawn point
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
