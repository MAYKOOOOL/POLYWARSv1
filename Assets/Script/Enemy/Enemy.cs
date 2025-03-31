using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 10; 
    public GameObject[] lootPrefabs;
    public GameObject deathEffect;
    public float dropChance = 0.5f;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy hit! Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DropLoot();
        Debug.Log("Enemy Died at: " + transform.position);

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        if (lootPrefabs.Length > 0 && Random.value < dropChance)
        {
            int randomIndex = Random.Range(0, lootPrefabs.Length);

            if (lootPrefabs[randomIndex] == null)
            {
                Debug.LogWarning("Loot prefab is missing or destroyed!");
                return;
            }

            GameObject loot = Instantiate(lootPrefabs[randomIndex], transform.position, Quaternion.identity);
            if (loot == null) return;

            Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * 5f, ForceMode2D.Impulse);
            }
        }
    }
}
