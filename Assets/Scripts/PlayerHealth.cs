/*
This script manages the health of an enemy character in a game. 
The enemy has a health bar that starts fully red and decreases with each hit 
Once the enemy’s health reaches zero or it takes a set number of hits, it dies, 
The health bar is positioned above the enemy and becomes visible upon taking damage.
This also triggering game events like adding score and rewards. 
*/
using UnityEngine;
using UnityEngine.UI;  // Required for Slider and Image components to control health bar

public class PlayerHealth : MonoBehaviour
{
    // Maximum health of the enemy
    public int maxHealth = 100;
    // Current health of the enemy
    private int currentHealth;
    // Amount of damage taken per hit
    public int damagePerHit = 10;
    // Counter to track the number of hits taken
    private int hitsTaken = 0;

    // Prefab reference for the health bar UI
    public GameObject healthBarPrefab;
    // Instance of the health bar object
    private GameObject healthBar;
    // Slider component for displaying health value
    private Slider healthBarSlider;
    // Image component for the fill color of the health bar
    private Image healthBarFillImage;
    // Visibility status of the health bar
    private bool isHealthBarVisible = false;

    // Reference to the endpoint position for checking enemy reaching goal
    public Transform endPoint;
    // Rewards for player when enemy is killed
    public int goldReward = 10;
    public int scoreReward = 100;

    void Start()
    {
        // Set the enemy's current health to the maximum health value
        currentHealth = maxHealth;

        // Instantiate the health bar above the enemy and make it a child of the enemy object
        healthBar = Instantiate(healthBarPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);

        // Retrieve the Slider component from the health bar object
        healthBarSlider = healthBar.GetComponentInChildren<Slider>();

        // Retrieve the Image component from the slider's fill area
        healthBarFillImage = healthBarSlider.fillRect.GetComponent<Image>();

        // Set the slider's maximum value to the maximum health and set it to the current health
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;

        // Initialize the health bar color to red
        healthBarFillImage.color = Color.red;

        // Hide the health bar initially
        healthBar.SetActive(false);
    }

    void Update()
    {
        // Update the health bar position to stay above the enemy as it moves
        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + new Vector3(0, 1, 0);
        }

        // Check if the enemy is close enough to the endpoint to trigger a game-over scenario
        if (Vector3.Distance(transform.position, endPoint.position) < 1f)
        {
            EndGame();
        }
    }

    // Method to handle damage taken by the enemy
    public void TakeDamage(int damage)
    {
        // If health bar is hidden, make it visible on taking damage
        if (!isHealthBarVisible)
        {
            ShowHealthBar();
        }

        // Increment the hit counter and reduce the current health by the damage amount
        hitsTaken++;
        currentHealth -= damage;

        // Update the health bar to reflect the new health level
        UpdateHealthBar();

        // If the enemy has health left, ensure the health bar color stays red
        if (currentHealth > 0)
        {
            UpdateHealthBarColor();
        }

        // If the enemy has been hit 10 times or health is zero, trigger the Die method
        if (hitsTaken >= 10 || currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to update the slider's value based on current health
    void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }

    // Method to display the health bar when taking damage for the first time
    void ShowHealthBar()
    {
        healthBar.SetActive(true);
        isHealthBarVisible = true;
    }

    // Method to set the health bar color to red consistently
    void UpdateHealthBarColor()
    {
        healthBarFillImage.color = Color.red;
    }

    // Method to handle the enemy's death process
    void Die()
    {
        // Log enemy death for debugging
        Debug.Log("Enemy Died");

        // Update enemy count in GameOverManager for tracking kills
        GameOverManager.instance.enemyCount++;
        Debug.Log("Enemies Killed: " + GameOverManager.instance.enemyCount);

        // Reward the player with gold and score for defeating the enemy
        GameManager.instance.AddGold(goldReward);
        GameManager.instance.AddScore(scoreReward);

        // Destroy the health bar and the enemy object upon death
        Destroy(healthBar);
        Destroy(gameObject);
    }

    // Method to handle game-over scenario if the enemy reaches the end
    void EndGame()
    {
        // Trigger game-over event in GameOverManager
        GameOverManager.instance.GameOver();

        // Destroy the enemy to remove it from the game scene
        Destroy(gameObject);
    }
}
