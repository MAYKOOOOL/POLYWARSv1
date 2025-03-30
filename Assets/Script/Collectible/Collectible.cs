using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Health, Shard }
    public CollectibleType type;
    public int value = 1;

    public int[] healingValue = { 3, 8 };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Health picked up!"); 
            HealthManager playerHealth = collision.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                int healAmount = healingValue[Random.Range(0, healingValue.Length)]; 
                Debug.Log("Healing player by " + healAmount);
                playerHealth.Heal(healAmount); 
            }

            Destroy(gameObject);
        }
    }
}
