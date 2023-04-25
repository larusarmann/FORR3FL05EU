using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public int startingLives = 3;

    private Rigidbody2D rb;
    private bool isGrounded;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private int lives;
    private Text livesText;

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
        // Move the player using the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Flip the player sprite based on the movement direction
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Check if the player is grounded
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Jump using the up arrow key or W key, if the player is grounded
        if (isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
                // Game over
                Debug.Log("Game Over");
            }

            UpdateLivesText();
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }
}
