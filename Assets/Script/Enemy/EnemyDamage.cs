using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int minDamage = 3;
    public int maxDamage = 6;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();

        if (playerHealth != null)
        {
            int damage = Random.Range(minDamage, maxDamage);
            playerHealth.TakeDamage(damage, transform);
        }
    }
}
