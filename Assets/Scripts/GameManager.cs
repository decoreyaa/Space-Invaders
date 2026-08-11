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
    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }
    void Update()
    {
        // only check while the game is actually still being played
        // (think: should this run if the player already lost?)
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
        GameOver,
        Won
    }

    public void SetGameOver()
    {
        Time.timeScale = 0f;
        currentState = GameState.GameOver;
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
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
}
