using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int keys = 0;
    private Text keysText;

    void Awake()
    {
        // Ensure that only one instance of GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keep GameManager object alive between scenes
        DontDestroyOnLoad(gameObject);

        // Find the keys text object in the scene
        keysText = GameObject.Find("KeysText").GetComponent<Text>();

        // Update the keys text
        UpdateKeysText();
    }

    public void AddKey()
    {
        keys++;
        UpdateKeysText();
    }

    void UpdateKeysText()
    {
        keysText.text = "Keys: " + keys;
    }

    // Add any other GameManager functionality here
}
