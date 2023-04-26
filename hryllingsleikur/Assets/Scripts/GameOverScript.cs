using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
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
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        // Hleður fyrstu senunni
        SceneManager.LoadScene("Level_1");
    }

    void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
