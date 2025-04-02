using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    private float currentHealth;

    public PlayerMovement playerMovement;

    private bool invincible = false;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float flickerSpeed = 0.1f;

    private SpriteRenderer[] spriteRenderers;

    private void Start()
    {
        gameObject.SetActive(true);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        currentHealth = maxHealth;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void TakeDamage(Transform damageSource = null)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.hit);
        if (invincible) return;

        int randomDamage = Random.Range(3, 5);

        currentHealth -= randomDamage;
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
        else
        {
            StartCoroutine(ActivateIFrames());
        }
    }

    public void TakeBossDamage(Transform damageSource = null)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.hit);
        if (invincible) return;

        int randomDamage = Random.Range(3, 5);

        currentHealth -= randomDamage;
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
        else
        {
            StartCoroutine(ActivateIFrames());
        }
    }


    private IEnumerator ActivateIFrames()
    {
        invincible = true;
        Debug.Log("iFrames Active!");

        float elapsedTime = 0;
        while (elapsedTime < invincibilityDuration)
        {
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.2f);
            }
            yield return new WaitForSeconds(flickerSpeed);

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
            yield return new WaitForSeconds(flickerSpeed);

            elapsedTime += flickerSpeed * 2;
        }

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }

        invincible = false;
    }

    public void Heal(float healAmt)
    {
        currentHealth += healAmt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;

        Debug.Log("Healed! Current Health: " + currentHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
