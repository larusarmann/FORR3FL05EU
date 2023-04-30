using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button startGameButton;
    public Button quitButton;

    void Start()
    {
        // Finnur takkana í senunni
        startGameButton = GameObject.Find("Start Game").GetComponent<Button>();
        quitButton = GameObject.Find("Quit").GetComponent<Button>();

        // bætir við Listeners til að chekka hvort það hafi verið ýtt á takkan
        startGameButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(Quit);
    }

    void StartGame()
    {
        // Hleður fyrstu senunni
        SceneManager.LoadScene("Level_1");
    }

    void Quit()
    {
        // hættir í leiknum
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
