using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Health, Shard }
    public CollectibleType type;
    public int value = 2;

    public int[] healingValue = { 3, 8 };

    private static int shardMax = 6;
    private static int currentShard = 0;
    private static int currentCoins = 0;

    public TextMeshProUGUI shardCounterText;
    public TextMeshProUGUI coinCounterText;

    private void Start()
    {
        UpdateShardUI();
        UpdateCoinUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            HealthManager playerHealth = collision.GetComponent<HealthManager>();

            AudioManager.instance.PlaySFX(AudioManager.instance.pickUp);

            if (type == CollectibleType.Health && playerHealth != null)
            {
                int healAmount = healingValue[Random.Range(0, healingValue.Length)];
                playerHealth.Heal(healAmount);
                Debug.Log("Healing player by " + healAmount);
            }
            else if (type == CollectibleType.Coin)
            {
                CoinManager.Instance.AddCoins(value);
                Debug.Log("Coin collected! Total: " + CoinManager.Instance.currentCoins);
            }
            else if (type == CollectibleType.Shard)
            {
                currentShard++;
                Debug.Log("Shard Collected! Total: " + currentShard + "/" + shardMax);
                UpdateShardUI();

                if (currentShard >= shardMax && playerHealth != null)
                {
                    Debug.Log("All shards collected! Health fully restored!");
                    playerHealth.Heal(playerHealth.maxHealth);

                    Projectile.ActivateDoubleDamage();
                    player.ActivateShardPowerUp();
                }
            }

            Destroy(gameObject);
        }
    }

    private void UpdateShardUI()
    {
        if (shardCounterText != null)
        {
            shardCounterText.text = "VET SHARD : " + currentShard + " / " + shardMax;
        }
    }

    private void UpdateCoinUI()
    {
        Debug.Log("Coin Count: " + currentCoins);

        if (coinCounterText != null)
        {
            coinCounterText.text = ": " + currentCoins;
        }
    }
}
