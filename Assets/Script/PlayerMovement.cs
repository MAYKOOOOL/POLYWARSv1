using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    GameObject Background;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask trapLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private HealthManager hm;
    private BoxCollider2D boxCollider;

    private Vector2 mousePosition;

    private float horizontalInput;
    private float wallJumpCooldown;

    [SerializeField] public float KBForce = 10f;
    [SerializeField] public float KBCounter;
    [SerializeField] public float KBTotalTime = 0.5f;

    public bool KnockFromRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        hm = FindObjectOfType<HealthManager>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
/*        Background.transform.localScale = new Vector3(1, 1, 1);*/

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // para mag flip ang character
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

       /* if (mousePosition.x < transform.position.x)
        {

            flipCharacter(true);
        }
        else if(mousePosition.x > transform.position.x)
        {
            flipCharacter(false);
        }*/
       
        // sprint
        if(Input.GetKey(KeyCode.LeftShift)) 
        {
            speed = 20f;
        }
        else
        {
           speed = 10f;
        }


        // animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        // para sa wall jump
        if(wallJumpCooldown > 0.2f)
        { 
            if(KBCounter <= 0)
            {
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            }
            else
            {
                if(KnockFromRight == true)
                {
                    rb.velocity = new Vector2(-KBForce, KBForce);
                }
                if(KnockFromRight == false)
                {
                    rb.velocity = new Vector2(KBForce, KBForce);
                }

                KBCounter -= Time.deltaTime;
            }


            if (onWall() && !isGrounded() && !isTrapped())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 2;
            }

            if (Input.GetKey(KeyCode.Space))
                Jump();

        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        /*print(onWall());*/
    }

    private void Jump()
    {
        if (isGrounded() || isTrapped())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        else if (onWall() && !isGrounded()) 
        {
            if(horizontalInput == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;

        }


    }

    /*private void flipCharacter(bool flip)
    {
        if (flip)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip character to the left
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Flip character to the right
        }

        // Prevent the background from flipping by resetting its scale
        if (Background != null)
        {
            Background.transform.localScale = new Vector3(1, 1, 1); // Reset background scale
        }
    }*/


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
           KBCounter = KBTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                KnockFromRight = true;
            }

            if (collision.transform.position.x > transform.position.x)
            {
                KnockFromRight = false;
            }

            int randomDamage = Random.Range(3, 5 + 1);
            hm.TakeDamage(randomDamage);
           
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isTrapped()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, trapLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 &&  isGrounded() && !onWall();
    }   
}
