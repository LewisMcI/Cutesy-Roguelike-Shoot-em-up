using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthbar();
    }
    [SerializeField] SpriteRenderer sRenderer;
    public void TakeDamage(float damage)
    {
        AudioManager.instance.PlaySFX("PlayerHit");
        StartCoroutine(DamageSpriteAnim());
        // Decrease curr health
        currentHealth -= damage;
        UpdateHealthbar();
        // Check if the enemy health is depleted
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Example method to call when the enemy dies
    private void Die()
    {
        // Destroy the enemy GameObject
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [SerializeField] private Transform healthbar;
    private void UpdateHealthbar()
    {
        if (currentHealth < 0f)
            healthbar.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        else if (currentHealth > maxHealth)
            healthbar.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            healthbar.localScale = new Vector3(currentHealth/maxHealth, 1.0f, 1.0f);
    }

    IEnumerator DamageSpriteAnim()
    {
        sRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sRenderer.color = Color.white;
    }
}
