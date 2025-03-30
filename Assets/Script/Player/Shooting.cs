/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInst;

    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void Start()
    {
        gun.transform.localScale = new Vector3(0.1791739f, 0.1791739f, 0.1791739f);
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = gun.transform.localScale;
        localScale.x = Mathf.Abs(localScale.x);  // Keep X positive
        localScale.y = (angle > 80 || angle < -80) ? -0.1791739f : 0.1791739f;

        gun.transform.localScale = localScale;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
            Projectile bulletMovement = bulletInst.GetComponent<Projectile>();
            if (bulletMovement != null)
            {
                bulletMovement.Initialize(direction);  
            }
        }
    }
}
*/