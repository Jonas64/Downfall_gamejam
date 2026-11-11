using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Drag your Enemy Prefab into this field in the Unity Inspector
    public GameObject enemyPrefab;

    // Time between each spawn (in seconds)
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        // Spawns the enemy at the position of the Spawner object
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}