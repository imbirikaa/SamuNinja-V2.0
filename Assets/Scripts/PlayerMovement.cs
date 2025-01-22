using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject Sspawn;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    GameObject arrow;
    [SerializeField] private float arrowSpeed = 5f;

    public GameObject gameOverPanel;

    float horizontalInput;
    public int arrows = 0;
    float moveSpeed = 5f;
    bool isFacingRight = false;
    float jumpPower = 5.5f;
    int health = 100;

    int score = 0;
    bool isGrounded = false;

    bool isAttacking = false;


    Spawn spawn1;

    Rigidbody2D rb;
    Animator animator;




    void Start()
    {
        UpdateHealthText();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        attack2();

        if (Input.GetKeyDown(KeyCode.B) && arrows > 0)
        {
            FireBullet();
            arrows--;

        }
        UpdateHealthText();

        if (score >= 100 && SceneManager.GetActiveScene().buildIndex == 1) // Check if in Level 1
        {
            SceneManager.LoadScene(2);
        }

    }
    public void reFill()
    {
        GetComponent<PlayerAttackSound>().PlayHealSound();
        health = 100;
        UpdateHealthText();
    }
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString() + " | Arrows: " + arrows.ToString() + " | Score: " + score.ToString(); // Update the UI Text
        }
        else
        {
            Debug.LogWarning("HealthText UI element is not assigned!");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput > 0f || !isFacingRight && horizontalInput < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy1")
        {
            if (Math.Abs(transform.position.x - collision.transform.position.x) < 1.8f)
            {

                attack2();
                if (isAttacking)
                {
                    EnemyControls enemyControls = collision.GetComponent<EnemyControls>();
                    enemyControls.death();

                }
            }


        }
    }

    public void getScore()
    {
        score += 10;
        UpdateHealthText();
    }
    private void attack2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            if (isAttacking) return;
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            GetComponent<PlayerAttackSound>().PlayAttackSound();

            Invoke("stopAttacking", 0.2f);
        }
    }

    void stopAttacking()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void death()
    {

        health -= 20;
        animator.SetBool("isHit", true);
        UpdateHealthText();
        Invoke("endAnim", 0.1f);
        if (health <= 0)
        {
            kill();
        }
    }

    void endAnim()
    {
        animator.SetBool("isHit", false);
    }

    void kill()
    {

        gameOverPanel.SetActive(true);
        spawn1 = Sspawn.GetComponent<Spawn>();
        spawn1.StopSpawning();
        healthText.text = "";

        Destroy(gameObject);
    }

    public void addArrow()
    {
        GetComponent<PlayerAttackSound>().PlayConsumeSound();
        arrows += 3;
        UpdateHealthText();
    }

    public bool isRight()
    {
        return isFacingRight;
    }

    void FireBullet()
    {

        GameObject bullet = Instantiate(arrow, transform.position + new Vector3(0f, -0.5f, 0), Quaternion.identity);


        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletDirection = !isFacingRight ? Vector2.right : Vector2.left;
        Arrow ar = bullet.GetComponent<Arrow>();
        if (!isFacingRight)
        {
            ar.FlipSprite();
        }


        rb.velocity = bulletDirection * arrowSpeed;

    }
}
