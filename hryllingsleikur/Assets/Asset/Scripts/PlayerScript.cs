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
    public float groundCheckOffset = 0.05f;
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
    // setur hversu mörg líf hann byrjar me og finnur textan á canvasinu
    lives = startingLives;
    livesText = GameObject.Find("LivesText").GetComponent<Text>();

    // uppfærir líf textan til að sýna hversu mörg líf hann byrjar með
    UpdateLivesText();
}

void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");

    rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

    // flippar playerinum eftir þvi hvaða átt hann snýr
    if (horizontalInput > 0)
    {
        spriteRenderer.flipX = false;
    }
    else if (horizontalInput < 0)
    {
        spriteRenderer.flipX = true;
    }

    // ef playerinn er ekki ódrepanlegur og blikkandi, setur material playersins í flash material
    if (!isInvincible && isFlashing)
    {
        spriteRenderer.material = flashMaterial;
    }

    Vector2 raycastOrigin = boxCollider.bounds.center - new Vector3(0, boxCollider.bounds.extents.y - groundCheckOffset, 0);

    // tjekka hvort playerinn sé á tilemapinu
    RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, boxCollider.bounds.extents.y + groundCheckDistance, groundLayer);
    isGrounded = hit.collider != null;

    // ef playerinn er á tilemapinu og það er ýtt á jump takkan, þá hoppar playerinn
    if (isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // ef playerinn dettur undir death height þá loadar gameover senan
    if (transform.position.y < deathHeight)
    {
        SceneManager.LoadScene("GameOver");

        // uppfærir lífatextan til að displaya núverandi líf
        UpdateLivesText();
    }

    // ef playerinn reachar endan af levelinu þá loadar næsta level
    if (transform.position.x >= 42 || transform.position.x <= -18)
    {
        SceneManager.LoadScene("Level_2");
    }

    // ef playerinn blikkar, set materialið á flash material
    if (isFlashing)
    {
        spriteRenderer.material = flashMaterial;
    }
}

    private void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Monster"))
    {
        // minnkar líf playersins um 1
        lives--;

        //chekkar hvort playerinn eigi líf eftir
        if (lives <= 0)
        {
            // Loadar "GameOver" senuni
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Respawnar monsterið
            other.gameObject.transform.position = new Vector3(-15, -2, 0);

            // gerir playerinn ódrepanlegan í 1 sekundu
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
