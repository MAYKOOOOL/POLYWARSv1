using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCD;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCD && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    /*    private void Attack()
        {
            animator.SetTrigger("Attack");
            cooldownTimer = 0;

            firePoint.localPosition = new Vector3(Mathf.Sign(transform.localScale.x) * Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);

            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));

        }*/

    private void Attack()
    {
        animator.SetTrigger("Attack");
        cooldownTimer = 0;

        // Set FirePoint position based on player's direction
        firePoint.localPosition = new Vector3(Mathf.Sign(transform.localScale.x) * Mathf.Abs(firePoint.localPosition.x),
                                              firePoint.localPosition.y,
                                              firePoint.localPosition.z);

        // Get the fireball and set its position & direction
        GameObject fireball = fireballs[FindFireball()];
        fireball.transform.position = firePoint.position;
        fireball.GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));

        
    }


    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }

        return 0;
    }



}
