using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBar; 

    private bool invincible = false; 
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float flickerSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void TakeDamage(float damage)
    {
        if (invincible) return; 

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();

        StartCoroutine(ActivateIFrames());
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth; 
        }
    }

    private void Die()
    {
        Destroy(gameObject); 
    }

    private IEnumerator ActivateIFrames()
    {
        invincible = true;
        float elapsedTime = 0;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            yield return new WaitForSeconds(flickerSpeed);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            yield return new WaitForSeconds(flickerSpeed);

            elapsedTime += flickerSpeed * 2;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        invincible = false;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }
}
