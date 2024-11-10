/*
THis deals with 
- player’s score and gold balance, 
- updating the UI elements that display score and balance values. 
- It also allows for modifying the score and gold based on in-game events like earning points or 
  purchasing items. 
*/

using UnityEngine;
using TMPro;        //because, using TextMeshPro components

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI goldText;

    public int score = 0;
    public int gold = 300;

    public static GameManager instance;

    // Awake method to initialize the Singleton pattern (design pattern that ensures that class has only one instance)
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Reinitialize the UI references when the scene is restarted
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        goldText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();

        UpdateScoreText();
        UpdateGoldText();    
   
    }

    void Update()
    {
        UpdateScoreText();
        UpdateGoldText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void AddGold(int coins)
    {
        gold += coins;
        UpdateGoldText();
    }

    public bool CanAfford(int cost)
    {
        return gold >= cost;
    }

    public void SpendGold(int cost)
    {
        if (CanAfford(cost))
        {
            gold -= cost;
            UpdateGoldText();
        }
        else
        {
            Debug.LogWarning("Not enough gold to buy the tower.");
        }
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold.ToString();
        }
    }

    public void Reset()
    {
        score = 0;  // Reset the score to its initial value
        gold = 300; // Reset to initial gold value
        UpdateScoreText();
        UpdateGoldText();
    }
    public int GetScore()
{
    return score;  // Return the current score
}

}
