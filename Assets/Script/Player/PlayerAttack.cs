using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCD = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private ParticleSystem attackEffect;

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
        if (Input.GetKey(KeyCode.J)  && cooldownTimer > attackCD && playerMovement.canAttack())
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
        Debug.Log("Attack triggered!");
        animator.SetTrigger("Attack");
        cooldownTimer = 0;

        if (attackEffect != null)
        {
            attackEffect.Stop();
            attackEffect.transform.position = firePoint.position;
            attackEffect.Play(); 
        }


        float direction = Mathf.Sign(transform.localScale.x);

        GameObject fireball = fireballs[FindFireball()];
        fireball.transform.position = firePoint.position;
        fireball.GetComponent<Projectile>().setDirection(direction);

        if (fireball.GetComponent<Projectile>().launchEffect != null)
        {
            ParticleSystem effect = Instantiate(fireball.GetComponent<Projectile>().launchEffect, firePoint.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 1f);
        }
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
