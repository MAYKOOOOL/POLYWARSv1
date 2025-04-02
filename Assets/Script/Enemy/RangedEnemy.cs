using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;  // Assign the projectile prefab
    public Transform firePoint;          // The point where the projectile will be fired
    public Transform playerTransform;    // The player's transform
    public float detectionRange = 5f;    // How far the enemy can detect the player
    public float fireRate = 2f;          // Time between each shot
    private float nextFireTime = 0f;

    void Update()
    {
        DetectAndShoot();
    }

    private void DetectAndShoot()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange && Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + fireRate; // Set the next fire time
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        AudioManager.instance.PlaySFX(AudioManager.instance.rangedEnemyProjectile);

        if (rb != null)
        {
            Vector2 direction = (playerTransform.position - firePoint.position).normalized;
            rb.velocity = direction * 10f * 2; // Adjust speed as needed
        }
    }
}

