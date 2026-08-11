using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemyGridSpawner enemySpawn;
    public GameState currentState = GameState.Playing;
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        Debug.Log("hasSpawned: " + enemySpawn.HasSpawned() + " count: " + enemySpawn.GetEnemyCount() + " state: " + currentState);
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
        currentState = GameState.GameOver;
        Debug.Log("game is over");
    }

    public void SetWon()
    {
        currentState = GameState.Won;
        Debug.Log("Player won");
    }
}
