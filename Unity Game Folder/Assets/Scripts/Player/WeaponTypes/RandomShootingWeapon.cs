using UnityEngine;

public class RandomShootingWeapon : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet to shoot
    public Transform firePoint; // Point from which the bullets are fired

    private float nextFireTime;
    public PlayerStats playerStats; // Reference to the PlayerStats script

    private void Start()
    {
        // Initialize nextFireTime to allow immediate firing
        nextFireTime = Time.time;

        // Get the PlayerStats component from the player GameObject
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found. Make sure it is attached to the player GameObject.");
        }
    }

    private void Update()
    {
        // Check if it's time to shoot
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / playerStats.atkSpdMultiplier; // Update next fire time based on fire rate
        }
    }

    private void Shoot()
    {
        AudioManager.instance.PlaySFX("PlayerShoot");
        // Calculate random shooting angle within the range
        float randomAngle = Random.Range(-playerStats.shootAngleRange, playerStats.shootAngleRange);

        // Calculate direction based on random shooting angle
        Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * firePoint.right;

        // Instantiate bullet prefab and set its direction, damage, and speed
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
        bullet.GetComponent<Bullet>().SetDamage(playerStats.damageMultiplier);
        bullet.GetComponent<Bullet>().SetSpeed(playerStats.bulletSpeedMultiplier);
    }
}
