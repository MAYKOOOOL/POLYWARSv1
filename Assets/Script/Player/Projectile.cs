using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float direction;
    private bool hit;
    private float lifetime;
    private Animator anim;
    [SerializeField] private BoxCollider2D boxCollider;

    private static bool doubleDamage = false; // Track if damage is doubled

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        lifetime += Time.deltaTime;
        if (lifetime > 2)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");

        if (collision.CompareTag("Enemy")) // Ensure the enemy has the "Enemy" tag
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                int baseDamage = Random.Range(3, 6); // Random damage between 3 and 5
                int finalDamage = doubleDamage ? baseDamage * 2 : baseDamage; // Double damage if shards are collected

                Debug.Log("Dealing " + finalDamage + " damage to enemy!");
                enemy.TakeDamage(finalDamage);
            }
        }
    }

    public void setDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        hit = false;
        boxCollider.enabled = true;
        gameObject.SetActive(true);

        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void Deactivate()
    {
        lifetime = 3;
        gameObject.SetActive(false);
    }

    public static void ActivateDoubleDamage()
    {
        doubleDamage = true; // Enables double damage
        Debug.Log("Double Damage Activated!");
    }
}
