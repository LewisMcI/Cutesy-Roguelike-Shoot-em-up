using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction; // Direction in which the bullet moves
    private float damage; // Damage inflicted by the bullet
    private float speed;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy with EnemyHealth script
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Inflict damage on the enemy and destroy the bullet
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
