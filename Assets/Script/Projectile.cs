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
        if(lifetime > 5)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");

        if (collision.CompareTag("Enemy")) // Make sure the enemy has the "Enemy" tag
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Deal 1 damage
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

        // Instead of flipping rotation, flip localScale (ensures direction works properly)
        transform.localScale = new Vector3(direction, 1, 1);
    }


    private void Deactivate()
    {
        lifetime = 3;
        gameObject.SetActive(false);
    }

}
