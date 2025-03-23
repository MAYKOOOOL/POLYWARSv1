using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float HealthAmount = 100f;
    public float health;

    private void Start()
    {
        health = HealthAmount;
    }
    public void TakeDamage(float damage)
    {
        HealthAmount -= damage;
        healthBar.fillAmount = HealthAmount / 100f;

        if(HealthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Heal(20);
        }
    }
    public void Heal(float healAmt)
    {
        HealthAmount += healAmt;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, 100);

        healthBar.fillAmount = HealthAmount / 100f;
    }
}


