/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject gun;

    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    private void Update()
    {
        HandleGunRotation();
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if (angle > 80 || angle < -80)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        gun.transform.localScale = localScale;
    }
}
*/