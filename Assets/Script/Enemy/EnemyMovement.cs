using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public float patrolDestination;
    public Transform playerTransform;
    public float chaseDistance = 5f;

    private bool isChasing;

    void Update()
    {
        DetectPlayer();

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }


    }

    private void DetectPlayer()
    {
        if (playerTransform == null) return; // Check if playerTransform is null before using it

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        isChasing = distanceToPlayer < chaseDistance;
    }

    private void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Patrol()
    {
        if (patrolPoints.Length < 2) return;

        Transform targetPoint = patrolPoints[(int)patrolDestination];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            patrolDestination = patrolDestination == 0 ? 1 : 0;
        }
    }
}
