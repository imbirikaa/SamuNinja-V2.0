using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
public class EnemyControls : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 2f;
    public float flipThreshold = 0.1f;

    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private bool sound = false;
    private float attackCooldown = 1f;
    private float lastAttackTime = -1f;
    bool isAttacking = false;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
    }

    void Update()
    {
        MoveTowardsPlayer();
        FlipSprite();
    }

    public void AssignPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    void MoveTowardsPlayer()
    {




        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;


            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            animator.SetBool("isMoving", true);

        }

    }

    void FlipSprite()
    {

        if (isFacingRight && transform.position.x > player.position.x || !isFacingRight && transform.position.x < player.position.x)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void death()
    {
        animator.SetBool("isDead", true);

        deathPros();
    }
    public void deathPros()
    {
        if (!sound)
        {
            sound = true;
            GetComponent<EnemySounds>().PlayDeathSound();

        }

        Invoke("kill", 1f);


    }

    void kill()
    {

        PlayerMovement pl = player.transform.GetComponent<PlayerMovement>();
        if (pl != null)
        {
            pl.getScore();
        }
        Destroy(gameObject);


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !isAttacking)
        {
            PlayerMovement pl = other.GetComponent<PlayerMovement>();
            if (Math.Abs(transform.position.x - other.transform.position.x) < 1.8f)
            {
                if (pl != null)
                {
                    if (Time.time >= lastAttackTime + attackCooldown)
                    {
                        animator.SetBool("isAttacking", true);
                        isAttacking = true;
                        GetComponent<EnemySounds>().PlayAttackSound();
                        pl.death();
                        Invoke("resetAttack", 0.3f);


                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }
    void resetAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

}
