using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Transform target; // Assign your player here
    public Vector2 minBounds, maxBounds; // Manually set min and max bounds
    public float smoothSpeed = 5f; // Adjust camera follow speed

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position;
        desiredPosition.z = transform.position.z; // Keep original camera depth

        // Clamp the position to stay within bounds
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // Smoothly move the camera to the clamped position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
