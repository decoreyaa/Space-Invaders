using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }
    public GameState currentState = GameState.Playing;
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
