using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float enemyLife = 30f;
    private NavMeshAgent agent;  // NavMeshAgent to control the movement

    public Transform player;     // The player transform, or any target you want the enemy to follow

    private void Start()
    {
        // Get the NavMeshAgent component attached to the enemy
        agent = GetComponent<NavMeshAgent>();

        // Set up the agent's speed, turning, etc. (optional)
        agent.speed = 3f;
        agent.angularSpeed = 120f;
    }

    private void Update()
    {
        // Move the enemy towards the player's position (or any other target) if the agent is valid
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);  // Set the player's position as the target destination
        }
    }

    // Detects collision with bullets
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))  // Make sure the bullet has a tag "Bullet"
        {
            Destroy(gameObject);  // Destroy the enemy when hit by the bullet
            Destroy(other.gameObject);  // Destroy the bullet
        }
    }
}
