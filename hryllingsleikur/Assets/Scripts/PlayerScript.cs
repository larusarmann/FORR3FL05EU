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
    public float deathHeight = -25f; // updated death height
    public LayerMask groundLayer;
    public int startingLives = 3;
    


    private Rigidbody2D rb;
    private bool isGrounded;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private int lives;
    private Text livesText;
    private bool hasKey = false;

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

        // Check if the player falls below the death height
        if (transform.position.y < deathHeight)
        {
                // Load the "GameOver" scene
                SceneManager.LoadScene("GameOver");
            

            UpdateLivesText();
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
            }

            UpdateLivesText();
        }
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
