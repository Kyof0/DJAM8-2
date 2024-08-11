using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int numberOfNewEnemies = 3; // Number of enemies to spawn per wave
    private int wavesSpawned = 0; // Counter to keep track of the number of waves spawned
    public int maxWaves = 3; // Maximum number of waves (initial + 2 extra)
    public List<GameObject> wave_enemies; // List of enemies for the wave

    void Update()
    {
        if (wavesSpawned < maxWaves && IsOnlyOneEnemyLeft())
        {
            SpawnEnemies();
            wavesSpawned++; // Increment the wave counter
        }
        if(wavesSpawned > maxWaves)
        {
            AudioManager.Instance.PlaySFX("win");

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
        // Check if there are no active enemies left
        return activeEnemyCount == 0;
    }

    private void SpawnEnemies()
    {
        int enemiesActivated = 0; // Counter to track how many enemies have been activated

        for (int i = 0; i < wave_enemies.Count; i++)
        {
            if (wave_enemies[i] == null) // Skip if the enemy has been destroyed
            {
                continue;
            }

            if (!wave_enemies[i].activeInHierarchy) // Only activate inactive enemies
            {
                wave_enemies[i].SetActive(true);
                enemiesActivated++;

                // If the required number of enemies have been activated, break out of the loop
                if (enemiesActivated >= numberOfNewEnemies)
                {
                    break;
                }
            }
        }
    }
}
