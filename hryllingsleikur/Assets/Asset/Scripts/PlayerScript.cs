using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float groundCheckDistance = 0.1f;
    public float deathHeight = -25f;
    public float groundCheckOffset = 0.05f; // added offset to ground check
    public LayerMask groundLayer;
    public int startingLives = 3;
    public Material flashMaterial;
    public float invincibilityDuration = 1f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private int lives;
    private Text livesText;
    private bool hasKey = false;
    private bool isFlashing = false;
    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lives = startingLives;
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        UpdateLivesText();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (!isInvincible && isFlashing)
    {
        spriteRenderer.material = flashMaterial;
    }

        // added ground check offset to starting point of raycast
        Vector2 raycastOrigin = boxCollider.bounds.center - new Vector3(0, boxCollider.bounds.extents.y - groundCheckOffset, 0);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, boxCollider.bounds.extents.y + groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (transform.position.y < deathHeight)
        {
            SceneManager.LoadScene("GameOver");

            UpdateLivesText();
        }
        if (transform.position.x >= 42 || transform.position.x <= -18)
        {
            SceneManager.LoadScene("Level_2");
        }


        if (isFlashing)
        {
            spriteRenderer.material = flashMaterial;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Monster"))
    {
        // Decrease the player's lives by 1
        lives--;

        // Check if the player has run out of lives
        if (lives <= 0)
        {
            // Load the "GameOver" scene
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Respawn the monster
            other.gameObject.transform.position = new Vector3(-15, -2, 0);

            // Make the player invincible for 1 second
            StartCoroutine(InvincibilityCoroutine());
        }

        UpdateLivesText();
    }
}

IEnumerator InvincibilityCoroutine()
{
    isInvincible = true;

    yield return new WaitForSeconds(1f);

    isInvincible = false;
}

    IEnumerator FlashPlayer()
{
    isFlashing = true;

    Material originalMaterial = spriteRenderer.material;

    spriteRenderer.material = flashMaterial;

    yield return new WaitForSeconds(0.1f);

    spriteRenderer.material = originalMaterial;

    isFlashing = false;
}

    public bool HasKey()
    {
        return hasKey;
    }

    public void AddKey()
    {
        hasKey = true;
    }

    void UpdateLivesText()
    {
        livesText.text = "Líf: " + lives;
    }
}
