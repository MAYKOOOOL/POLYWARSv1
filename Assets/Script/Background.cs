using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform player; 
    public float parallaxFactor = 0.5f; 
    public float smoothSpeed = 1.5f;

    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - player.position;
    }

    void Update()
    {
        Vector3 targetPosition = player.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

    }
}
