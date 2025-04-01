using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For Image

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBar; // Assign in Inspector (Image instead of Slider)

    private bool invincible = false; // Invincibility flag
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float flickerSpeed = 0.1f;

    private SpriteRenderer spriteRenderer; // For visual effects

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the boss's sprite renderer
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        if (invincible) return; // If boss is invincible, ignore the damage

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();

        // Trigger invincibility frames after taking damage
        StartCoroutine(ActivateIFrames());
    }

    // Update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth; // Update fillAmount of the Image
        }
    }

    // Handle the boss's death
    private void Die()
    {
        // Play death animation or effects here (optional)
        Destroy(gameObject); // Destroy the boss when health reaches 0
    }

    // Coroutine for invincibility frames (iFrames)
    private IEnumerator ActivateIFrames()
    {
        invincible = true;
        float elapsedTime = 0;

        // Flicker effect (similar to the player's invincibility frames)
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            yield return new WaitForSeconds(flickerSpeed);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            yield return new WaitForSeconds(flickerSpeed);

            elapsedTime += flickerSpeed * 2;
        }

        // Reset the sprite opacity back to normal
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        invincible = false;
    }
}
