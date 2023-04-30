using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    public float jumpForce = 300.0f;
    public float speedIncreaseInterval = 5.0f;
    public float speedIncreaseAmount = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        InvokeRepeating("IncreaseSpeed", speedIncreaseInterval, speedIncreaseInterval);
    }

    void Update()
    {
        // Calculate the direction vector towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Check if there is an obstacle in front of the monster
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + direction, new Vector2(0.2f, 0.6f), 0);
        bool isObstacleInFront = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Obstacle"))
            {
                isObstacleInFront = true;
                break;
            }
        }

        // Move the monster towards the player
        if (isObstacleInFront)
        {
            // Jump over the obstacle
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("Jump");
        }
        else
        {
            // Move towards the player
            transform.position += direction * speed * Time.deltaTime;
        }

        // Flip the monster sprite based on the movement direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Increase the speed of the monster
    void IncreaseSpeed()
    {
        speed += speedIncreaseAmount;
    }
}
