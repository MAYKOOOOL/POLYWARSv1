using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float speed = 20f;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    public GameObject impactEffect; 
    public ParticleSystem launchEffect; 

    private static bool doubleDamage = false;

    void Start()
    {
        // Play the launch particle effect
        if (launchEffect != null)
        {
            ParticleSystem effect = Instantiate(launchEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 1f); // Destroy after 1 sec to clean up
        }
    }

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

        if (collision.CompareTag("Enemy")) 
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                int baseDamage = Random.Range(3, 6); 
                int finalDamage = doubleDamage ? baseDamage * 2 : baseDamage; 

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

        if (launchEffect != null)
        {
            ParticleSystem effect = Instantiate(launchEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 1f);     
        }
    }


    private void Deactivate()
    {
        lifetime = 3;
        gameObject.SetActive(false);
    }

    public static void ActivateDoubleDamage()
    {
        doubleDamage = true; 
        Debug.Log("Double Damage Activated!");
    }
}
