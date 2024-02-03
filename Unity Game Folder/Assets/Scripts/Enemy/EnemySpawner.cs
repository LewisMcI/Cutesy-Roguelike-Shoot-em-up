using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public float spawnInterval = 2f; // Interval between enemy spawns
    public float spawnRadius = 10f; // Spawn radius around the player
    public int maxEnemies = 10; // Maximum number of enemies spawned at once

    private List<GameObject> activeEnemies = new List<GameObject>();
    private float nextSpawnTime;

    private void Start()
    {
        // Start spawning enemies
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        // Check if it's time to spawn a new enemy and there are available slots
        if (Time.time >= nextSpawnTime && activeEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random position within the spawn radius
        Vector3 spawnPosition = player.position + Random.insideUnitSphere.normalized * spawnRadius;

        // Ensure enemies spawn at the same z-position as the player
        spawnPosition.z = 0f;

        // Instantiate a random enemy prefab
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemy);

        // Set the target for enemy movement
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetTarget(player);
        }

        // Subscribe to the enemy's despawn event
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyDeath += () => activeEnemies.Remove(enemy);
        }
    }
}
