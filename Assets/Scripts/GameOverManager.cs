/*
This script manages the end-of-level and game-over conditions, 
making sure the game recognizes when the player has either defeated all enemies (level cleared) 
or needs to display a game-over screen.
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    // Enemy count
    public int enemyCount = 0;
    public int allEnemies = 3;

    private bool isGameOver = false; // Add a flag to prevent repeated game over logic

    // Awake method to initialize the Singleton pattern (design pattern that ensures that class has only one instance)
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
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


    
public void GameOver()
{
    if (SceneManager.GetActiveScene().buildIndex != 2)  // Assuming scene 2 is the Game Over screen
    {
        SceneManager.LoadScene(2);  // gameOverScreen popup
        Debug.Log("Game Over! You lost!");
        Time.timeScale = 0;         // Stop the game
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
