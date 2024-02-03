using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Movement speed

    private Transform target; // Target to move towards
    private Rigidbody2D rb;

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
