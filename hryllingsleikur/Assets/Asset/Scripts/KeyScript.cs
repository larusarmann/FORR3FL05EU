using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Hurd;

    private void Start()
    {
        // Randomize the position of the key
        bool isCollidingWithTilemap;
        do
        {
            float x = Random.Range(-16f, 40f);
            float y = Random.Range(-2f, 5f);
            transform.position = new Vector3(x, y, 0);
            isCollidingWithTilemap = GetComponent<Collider2D>().OverlapPoint(transform.position);
        } while (isCollidingWithTilemap || transform.position.y < -1.5f);
        // The loop will continue if the key collides with the tilemap or spawns below the ground level
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the key
        if (other.CompareTag("Player"))
        {
            // Get the player script
            PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript>();

            // Check if the player already has the key
            if (!playerScript.HasKey())
            {
                // Add the key to the player's inventory
                playerScript.AddKey();

                // Destroy the Hurd Foreground GameObject
                GameObject hurdForeground = GameObject.Find("Hurd");
                if (hurdForeground != null)
                {
                    Destroy(hurdForeground);
                }

                // Show a console message
                Debug.Log("Player picked up the key!");

                // Destroy the key object
                Destroy(gameObject);
            }
        }
    }
}
