using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int keys = 0;
    private Text keysText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
        keysText = GameObject.Find("KeysText").GetComponent<Text>();
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
}
