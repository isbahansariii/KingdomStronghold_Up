using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    // Enemy count
    public int enemyCount = 0;
    public int allEnemies = 3;

    private bool isGameOver = false; // Add a flag to prevent repeated game over logic

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

void Update()
{
    // Check if the enemy count has reached the total enemies and the game is not over
    if (enemyCount >= allEnemies)
    {
        GameOver();
    }
}


    void LevelCleared()
    {
        isGameOver = true; // Set the flag to true to prevent further execution
        SceneManager.LoadScene(2); // Load Game Over or Level Cleared scene
        Debug.Log("Level Cleared! Total Score: " + GameManager.instance.GetScore());
    }


    
public void GameOver()
{
    if (SceneManager.GetActiveScene().buildIndex != 2)  // Assuming scene 2 is the Game Over screen
    {
        SceneManager.LoadScene(2); // gameOverScreen popup
        Debug.Log("Game Over! You lost!");
        Time.timeScale = 0; // Stop the game
    }
}

    public void DisplayFinalScore()
{
    int finalScore = GameManager.instance.GetScore();  // Get the score from GameManager
    Debug.Log("Level Cleared! Total Score: " + finalScore);
}

    public void ResetGameOverManager()
{
    enemyCount = 0; // Reset the enemy count
}

}
