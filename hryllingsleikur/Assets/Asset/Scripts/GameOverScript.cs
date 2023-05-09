using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Button startGameButton;
    public Button quitButton;

    void Start()
    {
        // finnur start game og quit takkana í senuni
        startGameButton = GameObject.Find("Start Game").GetComponent<Button>();
        quitButton = GameObject.Find("Quit").GetComponent<Button>();

        // bætir við listeners til að tékka hvort það sé búið að ýta á takkana
        startGameButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Byrjar leikin
    void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    // Loadar MainMenu senunni
    void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
