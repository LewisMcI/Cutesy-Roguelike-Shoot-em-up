using UnityEngine;
using System;
using System.Collections;

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
    [SerializeField] SpriteRenderer sRenderer;

    // Example method to call when the enemy takes damage
    public void TakeDamage(float damage)
    {
        StartCoroutine(DamageSpriteAnim());
        // Decrease enemy health
        currentHealth -= damage;

        // Check if the enemy health is depleted
        if (currentHealth <= 0f)
        {
            AudioManager.instance.PlaySFX("EnemyKilled");
            Die();
        }
        else
            AudioManager.instance.PlaySFX("EnemyHit");
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

    IEnumerator DamageSpriteAnim()
    {
        sRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sRenderer.color = Color.white;
    }
}
