using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rgdb;
    private bool explodindo = false;
    public Collider2D explosionCollider;

    private void Awake()
    {
        //anim = gameObject.GetComponent<Animator>();
        explosionCollider.enabled = false;
    }

    void Start()
    {
        rgdb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("explode", false);
    }

    void Update()
    {
        if (explodindo == true)
        {
            rgdb.velocity = Vector2.zero;
            rgdb.angularVelocity = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            explodindo = true;
            animator.SetBool("explode", explodindo);
            explosionCollider.enabled = true;
            Destroy(gameObject, 1);
        }

        if (collision.gameObject.tag == "Star")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
