/*
It manages the display of the final score, the outcome statement, 
and provides functionality for restarting the game or returning to the home screen. 
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreFinalText;
    public TextMeshProUGUI statement;
    public int winScore = 300;

    // Start is called before the first frame update
    void Start()
    {
        // Update the final score text when the game over screen is shown
        UpdateScText();
        updateStatement();
    }

    public void updateStatement()
    {
        // Convert scoreFinalText.text to an integer and compare it with winScore
        int score = GameManager.instance.GetScore();
            if (score != winScore)
            {
                statement.text = "Enemy entered into the Kingdom!!";
            }
            else
            {
                statement.text = "You killed all the enemies!";
            }
        }

public void homeBtnClicked()
{
    Time.timeScale = 1; // Ensure the game speed is reset
    GameOverManager.instance.ResetGameOverManager(); // Reset enemy count and any game-over-related variables
    GameManager.instance.Reset(); // Reset score and gold
    Destroy(GameOverManager.instance.gameObject); // Destroy the GameOverManager to prevent it from persisting
    SceneManager.LoadScene(0); // Load the home scene (assuming 0 is the home screen scene)
}


public void restartBtnClicked()
{
 RestartGame(); // Call the RestartGame method when restart button is clicked
}


    public void UpdateScText()
    {
        if (scoreFinalText != null)
        {
            scoreFinalText.text = "Score: " + GameManager.instance.GetScore().ToString();
        }
    }

   private void RestartGame()
    {
        Time.timeScale = 1; // Reset time scale to normal speed
        GameOverManager.instance.ResetGameOverManager(); // Reset enemy count and other game over conditions
        GameManager.instance.Reset(); // Reset score and gold
        Destroy(GameOverManager.instance.gameObject); // Destroy the GameOverManager to prevent it from persisting
        SceneManager.LoadScene(1); // Restart the game scene (assuming 1 is the gameplay scene)
        
    }

}
