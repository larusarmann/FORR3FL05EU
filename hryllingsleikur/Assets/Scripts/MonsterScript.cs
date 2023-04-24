using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    public float speedIncreaseInterval = 5.0f;
    public float speedIncreaseAmount = 0.5f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("IncreaseSpeed", speedIncreaseInterval, speedIncreaseInterval);
    }

    void Update()
    {
        // Calculate the direction vector towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the monster towards the player
        transform.position += direction * speed * Time.deltaTime;

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
