using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; // Movement speed of the player

    [Header("Sprite References")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private PlayerStats playerStats;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        InitializeComponents();
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void InitializeComponents()
    {
        // Initialize Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f; // Disable gravity

        // Initialize SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Subscribe to the OnLevelUp event to handle level up
        playerStats.OnLevelUp.AddListener(HandleLevelUp);
    }

    private void HandleLevelUp()
    {
        Debug.Log("Player Level Up");
    }

    private void HandleMovementInput()
    {
        // Get horizontal and vertical input from the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // Apply movement to the Rigidbody2D
        MovePlayer(movement);

        // Change sprite based on movement direction
        UpdateSprite(movement);
    }

    private void MovePlayer(Vector2 movement)
    {
        // Calculate velocity based on movement vector and speed
        Vector2 velocity = movement * speed;

        // Apply velocity to the Rigidbody2D
        rb.velocity = velocity;
    }

    private void UpdateSprite(Vector2 movement)
    {
        // Update sprite based on movement direction
        if (movement == Vector2.up)
            spriteRenderer.sprite = upSprite;
        else if (movement == Vector2.down)
            spriteRenderer.sprite = downSprite;
        else if (movement == Vector2.left)
            spriteRenderer.sprite = leftSprite;
        else if (movement == Vector2.right)
            spriteRenderer.sprite = rightSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with a pickable EXP drop
        if (other.CompareTag("PickableEXP"))
        {
            AudioManager.instance.PlaySFX("Pickup");
            // Get the EXP value from the drop
            int expValue = other.GetComponent<ExpDrop>().GetEXPValue();

            // Add the EXP to the player's stats
            playerStats.AddXP(expValue);

            // Destroy the pickable EXP drop
            Destroy(other.gameObject);
        }
    }
}
