
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemyGridSpawner enemySpawn;
    public GameState currentState = GameState.Playing;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject restartButton;
    public GameObject pauseMenuUI;
    public GameObject quitButton;
    public GameObject MainMenuButton;

    void Awake()
    {
        Instance = this;
        // a scene reload can arrive with timeScale still 0 from a pause or game over
        Time.timeScale = 1f;
    }
    void Start()
    {

    }
    void Update()
    {
        // Escape is handled outside the Playing guard on purpose - Pause() leaves the
        // Playing state, so a check inside it can never see the keypress that unpauses.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing) Pause();
            // explicit rather than a bare else, so Escape can't unfreeze a finished game
            else if (currentState == GameState.Pause) Resume();
        }

        if (currentState == GameState.Playing)
        {   
            if (enemySpawn.HasSpawned() && enemySpawn.GetEnemyCount() == 0)
                {
                    SetWon();
                }
        }
    // if spawner has finished spawning AND enemy count is 0 → SetWon()
    }
    public enum GameState
    {
        Playing,
        Pause,
        GameOver,
        Won
    }

    public void SetGameOver()
    {
        Time.timeScale = 0f;
        currentState = GameState.GameOver;
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
        Debug.Log("game is over");
        
    }

    public void SetWon()
    {
        Time.timeScale = 0f;
        currentState = GameState.Won;
        winText.SetActive(true);
        restartButton.SetActive(true);
        Debug.Log("Player won");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        currentState = GameState.Pause;
        MainMenuButton.SetActive(true);
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        MainMenuButton.SetActive(false);
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
