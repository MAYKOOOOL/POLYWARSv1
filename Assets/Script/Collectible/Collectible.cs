using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Health, Shard }
    public CollectibleType type;
    public int value = 1;

    public int[] healingValue = { 3, 8 };

    private static int shardMax = 6;  // Total shards required
    private static int currentShard = 0; // Track collected shards

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            HealthManager playerHealth = collision.GetComponent<HealthManager>();

            if (type == CollectibleType.Health && playerHealth != null)
            {
                int healAmount = healingValue[Random.Range(0, healingValue.Length)];
                playerHealth.Heal(healAmount);
                Debug.Log("Healing player by " + healAmount);
            }
            else if (type == CollectibleType.Coin)
            {
                Debug.Log("Coin collected!");
            }
            else if (type == CollectibleType.Shard)
            {
                currentShard++;
                Debug.Log("Shard Collected! Total: " + currentShard + "/" + shardMax);

                // If all shards are collected, max out health
                if (currentShard >= shardMax && playerHealth != null)
                {
                    Debug.Log("All shards collected! Health fully restored!");
                    playerHealth.Heal(playerHealth.maxHealth); // Heal to max

                    Projectile.ActivateDoubleDamage();
                }

            }

            Destroy(gameObject);
        }
    }
}
