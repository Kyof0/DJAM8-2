using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int numberOfNewEnemies = 3; // Number of enemies to spawn
    private bool hasSpawned = false; // Flag to ensure enemies are only spawned once per wave
    public List<GameObject> wave_enemies; // List of enemies for the wave

    void Update()
    {
        if (!hasSpawned && IsOnlyOneEnemyLeft())
        {
            SpawnEnemies();
            hasSpawned = true; // Mark the wave as spawned
        }
        
    }

    private bool IsOnlyOneEnemyLeft()
    {
        // Find all objects of type Enemy in the scene, including inactive ones
        Enemy[] enemies = FindObjectsOfType<Enemy>(true);

        // Count only active enemies
        int activeEnemyCount = 0;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                activeEnemyCount++;
            }
        }
        // Check if there is exactly one active enemy left
        return activeEnemyCount == 0;
    }

    private void SpawnEnemies()
    {
        foreach (GameObject enemy in wave_enemies)
        {
            enemy.SetActive(true); // Activate the enemy
        }
    }
}
