using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(transform);
            }
            else
            {
                Debug.LogWarning("HealthManager component not found on player!");
            }
        }
    }


}
