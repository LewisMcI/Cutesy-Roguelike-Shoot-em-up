using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public event Action OnEnemyDeath = delegate { };

    public float maxHealth = 100f; // Maximum health of the enemy
    public int expValue = 10; // EXP value dropped by the enemy
    public GameObject expPrefab; // Prefab of the EXP object to drop

    private float currentHealth; // Current health of the enemy

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    // Example method to call when the enemy takes damage
    public void TakeDamage(float damage)
    {
        // Decrease enemy health
        currentHealth -= damage;

        // Check if the enemy health is depleted
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Example method to call when the enemy dies
    private void Die()
    {
        // Drop EXP object
        Instantiate(expPrefab, transform.position, Quaternion.identity);

        // Perform death logic
        // For example, play death animation, particle effects, etc.

        // Invoke the OnEnemyDeath event
        OnEnemyDeath.Invoke();

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
