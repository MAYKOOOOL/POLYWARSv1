using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform playerTransform;
    public float detectionRange = 7f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    private BossHealth bossHealth; // Reference to the BossHealth script

    private bool hasEnteredRedArea = false;
    private float redAreaStartX = -3f;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();

        if (bossHealth != null)
        {
            bossHealth.TakeDamage(0);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        float playerX = playerTransform.position.x;

        if (playerX >= redAreaStartX && !hasEnteredRedArea)
        {
            hasEnteredRedArea = true;
        }

        if (hasEnteredRedArea && distanceToPlayer <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                ShootProjectiles();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void ShootProjectiles()
    {
        int projectileCount = 3;
        float angleStep = 15f;

        for (int i = 0; i < projectileCount; i++)
        {
            float angleOffset = (i - 1) * angleStep;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = (playerTransform.position - firePoint.position).normalized;
                direction = Quaternion.Euler(0, 0, angleOffset) * direction;
                rb.velocity = direction * 10f;
            }
        }
    }

    // Damage the boss through the BossHealth script
    public void TakeDamage(float damage)
    {
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
        }
    }
}
