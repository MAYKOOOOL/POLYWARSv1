using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private int damage;
    public HealthManager playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            int randomDamage = Random.Range(3, 5 + 1);
            playerHealth.TakeDamage(randomDamage);
        }
    }
}
