using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWhenNear : MonoBehaviour
{
    public float radius = 1.0f;
    public float damage = 10.0f;
    public float cooldown = .01f;
    public bool isPlayer = false;
    public LayerMask layers;
    private float nextTime = 0.0f;
    public void FixedUpdate()
    {
        if (Time.time < nextTime)
            return;

        RaycastHit2D[] hits;
        if (!isPlayer)
            hits = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero, radius/2, layers);
        else
            hits = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero, 0, layers);


        // Draw the circle cast hitbox
        foreach (RaycastHit2D hit in hits)
        {
            nextTime = Time.time + cooldown;

            Debug.Log("Hit");

            if (!isPlayer)
            {
                PlayerHealth playerHealth = GameObject.Find("PlayerHealth").GetComponent<PlayerHealth>();

                playerHealth.TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        // Draw the circle cast gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
