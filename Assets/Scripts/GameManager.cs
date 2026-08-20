
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for scene loading
using TMPro; // Needed for TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int currentWave = 1;
    public static GameManager Instance;
    public EnemyGridSpawner enemySpawn;
    public GameState currentState = GameState.Playing;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject restartButton;
    public GameObject pauseMenuUI;
    public GameObject quitButton;
    public GameObject MainMenuButton;
    public GameObject Volume; 
    public TMP_Text waveText;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    // shared with MainMenu so the two scenes can't drift apart on a rename
    public const string HighScoreKey = "HighScore";
    private int highScore; // best score ever recorded, loaded from PlayerPrefs
    private int score = 0;


    void Awake()
    {
        Instance = this;
        // load before Start() reads it for the display
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        // a scene reload can arrive with timeScale still 0 from a pause or game over
        Time.timeScale = 1f;
    }
    void Start()
    {
        waveText.text = "Wave: " + currentWave;
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
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
                    AddScore(currentWave * 100); // bonus for the wave just cleared
                    currentWave++;
                    waveText.text = "Wave: " + currentWave;
                    enemySpawn.SpawnWave(currentWave);
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
        MainMenuButton.SetActive(true);
        quitButton.SetActive(true);
        Debug.Log("game is over");
        SaveHighScore();

        AudioManager.Instance.PlayLose();
        
    }

    // public void SetWon()
    // {
    //     Time.timeScale = 0f;
    //     currentState = GameState.Won;
    //     winText.SetActive(true);
    //     restartButton.SetActive(true);
    //     MainMenuButton.SetActive(true);
    //     quitButton.SetActive(true);
    //     Debug.Log("Player won");
    // }

    public void RestartGame()
    {
        SaveHighScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        currentState = GameState.Pause;
        MainMenuButton.SetActive(true);
        quitButton.SetActive(true);
        Volume.SetActive(true);
        
        
    }

    public void Resume()
    {
        quitButton.SetActive(false);
        Volume.SetActive(false);
        pauseMenuUI.SetActive(false);
        MainMenuButton.SetActive(false);
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        SaveHighScore();
        Application.Quit();
    }

    public void MainMenu()
    {
        SaveHighScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void AddPoints(int points)
    {
        AddScore(points * currentWave); // kills are worth more in later waves
    }

    // Single owner of the score: the value, the HUD labels, and the record are
    // updated together so they can never disagree.
    private void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
        }
    }

    // highScore already tracks the run live, so this only has to persist it.
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

}
