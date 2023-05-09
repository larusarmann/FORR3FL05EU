using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Hurd;

    private void Start()
    {
        // Randomisar staðsetninguna á lyklinum
        bool isCollidingWithTilemap;
        do
        {
            float x = Random.Range(-16f, 40f);
            float y = Random.Range(-2f, 5f);
            transform.position = new Vector3(x, y, 0);
            isCollidingWithTilemap = GetComponent<Collider2D>().OverlapPoint(transform.position);
        } while (isCollidingWithTilemap || transform.position.y < -1.5f);
        // loopar áfram ef lykillin collidar við tilemappið eða fyrir neðan ground level
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // chekkar hvort playerinn collidi við lykilinn
        if (other.CompareTag("Player"))
        {
            // nær í playerscript
            PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript>();

            // Chekkar hvort playerinn sé með lykilinn
            if (!playerScript.HasKey())
            {
                // setur lykilinn í inventory
                playerScript.AddKey();

                // eyðileggur hurðina þegar hann snertir lykilinn
                GameObject hurdForeground = GameObject.Find("Hurd");
                if (hurdForeground != null)
                {
                    Destroy(hurdForeground);
                }

                // Sýnir í console að playerinn hafi pikkað upp lykilinn
                Debug.Log("Player picked up the key!");

                // eyðileggur lykilinn
                Destroy(gameObject);
            }
        }
    }
}
