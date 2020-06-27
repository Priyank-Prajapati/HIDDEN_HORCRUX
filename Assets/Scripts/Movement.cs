using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    int i = 0;
    bool isGrounded=true;
    [SerializeField]
    Transform GroundCheck;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Ground")
        {
            isGrounded = true;
        }
        else if(collision.collider.tag=="coin")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.collider.tag=="Lava")
        {
            print("Die");
            i = 1;
            animator.Play("Player_Die");
            StartCoroutine(ExampleCoroutine());

        }
    }
    IEnumerator ExampleCoroutine()
    {
        
        yield return new WaitForSeconds(2);
        rb2d.position = new Vector2(-21, -1);
        animator.Play("Idle");
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        i=0;


    }
    private void FixedUpdate()
    {
       
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(4, rb2d.velocity.y);
            animator.Play("Player_run");
            spriteRenderer.flipX = false;

        }
       else if (Input.GetKey("w"))
        {
            animator.Play("Attack");
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-4, rb2d.velocity.y);
            animator.Play("Player_run");
            spriteRenderer.flipX = true;
        }
        else if(i==0)
        {
            animator.Play("Idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        if(Input.GetKey("space") && isGrounded && i==0)
        {
            animator.Play("Jump");
            isGrounded = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 8);
        }
        
    }


}
