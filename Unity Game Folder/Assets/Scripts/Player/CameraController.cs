using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    private void Update()
    {
        // Check if player reference is set
        if (player == null)
        {
            Debug.LogWarning("Player reference is not set in CameraController.");
            return;
        }

        // Get the current position of the camera
        Vector3 newPosition = transform.position;

        // Set the camera's position to be centered on the player
        newPosition.x = player.position.x;
        newPosition.y = player.position.y;
        transform.position = newPosition;
    }
}
