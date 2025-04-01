using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    public float damage = 30f; 
    public float knockbackForce = 5f;
    public GameObject explosionEffect;
    public LayerMask playerLayer; 
    public float lifetime = 3f; 

    private float currentLifetime = 0f; 

    private void Start()
    {
        int bossProjectileLayer = gameObject.layer;
        int wallLayer = LayerMask.NameToLayer("Wall");
        int groundLayer = LayerMask.NameToLayer("Ground");

        if (wallLayer != -1)
            Physics2D.IgnoreLayerCollision(bossProjectileLayer, wallLayer, true);
        if (groundLayer != -1)
            Physics2D.IgnoreLayerCollision(bossProjectileLayer, groundLayer, true);
    }

    private void Update()
    {
        currentLifetime += Time.deltaTime;

        if (currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) 
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                Destroy(gameObject);
              
                playerHealth.TakeDamage(damage);

                ApplyKnockback(collision.gameObject);

                Explode();
            }
        }
    }

    private void ApplyKnockback(GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDirection = (target.transform.position - transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
