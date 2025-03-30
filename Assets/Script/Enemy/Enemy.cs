using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 10; // Enemy's health
    public GameObject[] lootPrefabs; // Assign different loot prefabs in the inspector
    public float dropChance = 0.5f;  // 50% chance to drop loot

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
        Debug.Log("Enemy died!");
        DropLoot();
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        if (lootPrefabs.Length > 0 && Random.value < dropChance) 
        {
            int randomIndex = Random.Range(0, lootPrefabs.Length);
            GameObject loot = Instantiate(lootPrefabs[randomIndex], transform.position, Quaternion.identity);

            Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * 5f, ForceMode2D.Impulse);
            }
        }
    }
}
