using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    private float currentHealth;

    public PlayerMovement playerMovement;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Transform damageSource = null)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.fillAmount = currentHealth / maxHealth;
        Debug.Log("Health: " + currentHealth);

        if (damageSource != null && playerMovement != null)
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = damageSource.position.x > transform.position.x;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmt)
    {
        currentHealth += healAmt;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        Debug.Log("Health: " + currentHealth);

        healthBar.fillAmount = currentHealth / 100f; 
    }


    private void Die()
    {
        Destroy(gameObject); 
    }
}
