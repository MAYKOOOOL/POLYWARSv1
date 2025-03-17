using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // How smoothly the camera moves
    public Vector3 offset;         // The offset from the player’s position to the camera

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set in CameraFollow script.");
        }
    }

    void FixedUpdate()
    {
        // Check if the player reference is set
        if (player != null)
        {
            // Calculate the desired position with offset
            Vector3 desiredPosition = player.position + offset;

            // Smoothly interpolate between the camera's current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera position
            transform.position = smoothedPosition;
        }
    }
}
