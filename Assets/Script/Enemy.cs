using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 10; // Enemy's health

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
        Destroy(gameObject); // Remove enemy from the scene
    }
}
