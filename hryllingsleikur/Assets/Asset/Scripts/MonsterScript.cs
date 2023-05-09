using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    // objectið sem monsterið eltir
    public Transform player;

    // hraðinn sem monsterið er með
    public float speed = 2.0f;

    // hoppkraftur monstersins
    public float jumpForce = 300.0f;

    // tími sem tekur aður enn að monsterið verður hraðara
    public float speedIncreaseInterval = 5.0f;

    // hversu mikill hraði bætist á monsterið
    public float speedIncreaseAmount = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // hækkar hraðan
        InvokeRepeating("IncreaseSpeed", speedIncreaseInterval, speedIncreaseInterval);
    }

    void Update()
    {
        // reiknar hvaða átt monsterið á að snúa
        Vector3 direction = (player.position - transform.position).normalized;

        // chekkar hvort það sé obstacle fyrir framan monsterið
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

        // færir monsterið í átt að playerinum
        if (isObstacleInFront)
        {
            // hoppar yfir obstacle
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("Jump");
        }
        else
        {
            // færa skrimslið að playerinum
            transform.position += direction * speed * Time.deltaTime;
        }

        // flippar monsterinu eftir hvaða átt monsterið er að hreyfast
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // gerir skrimslið hraðara
    void IncreaseSpeed()
    {
        speed += speedIncreaseAmount;
    }
}
