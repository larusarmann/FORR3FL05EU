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
            transform.position = new Vector3(Random.Range(-7f, 7f), 0, 0);
            isCollidingWithTilemap = GetComponent<Collider2D>().OverlapPoint(transform.position);
        } while (isCollidingWithTilemap);
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
