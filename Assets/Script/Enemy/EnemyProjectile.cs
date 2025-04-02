using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager playerHealth = collision.GetComponent<HealthManager>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(transform);
            Destroy(gameObject);
        }
    }


}
