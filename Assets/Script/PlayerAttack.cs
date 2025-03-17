using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCD;
    private Animator animator;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Attack()
    {
        animator.SetTrigger("Attack");
    }



}
