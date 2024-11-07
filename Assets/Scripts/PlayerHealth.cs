using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject healthBarPrefab;
    private GameObject healthBar;
    private Slider healthBarSlider;
    private bool isHealthBarVisible = false;

    // Reference to the end point
    public Transform endPoint;
    public int goldReward = 10;
    public int scoreReward = 100;


    void Start()
    {
        currentHealth = maxHealth;

        // Instantiate the health bar and set it as a child of the enemy
        healthBar = Instantiate(healthBarPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);

        // Find the Slider component
        healthBarSlider = healthBar.GetComponentInChildren<Slider>();

        // Initialize the slider's max value
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;

        // Start with the health bar hidden
        healthBar.SetActive(false);
    }

    void Update()
    {
        // Update the health bar's position to stay above the enemy
        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + new Vector3(0, 1, 0);
        }

        // Check if the enemy has reached the end point
        if (Vector3.Distance(transform.position, endPoint.position) < 1f)
        {
            EndGame();
        }


    }

    public void TakeDamage(int damage)
    {
        if (!isHealthBarVisible)
        {
            ShowHealthBar();
        }

        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        // Update the value of the slider
        healthBarSlider.value = currentHealth;

        // Ensure the health bar is visible when taking damage
        if (!isHealthBarVisible)
        {
            healthBar.SetActive(true);
            isHealthBarVisible = true;
        }
    }

    void ShowHealthBar()
    {
        healthBar.SetActive(true);
        isHealthBarVisible = true;
    }

    void Die()
    {
        // Handle player death
        Debug.Log("Player Died");

        // Increment enemy count in GameOverManager when an enemy dies
        GameOverManager.instance.enemyCount++;
        Debug.Log("Enemies Killed: " + GameOverManager.instance.enemyCount);

        // Add gold and score when the enemy dies
        GameManager.instance.AddGold(goldReward);
        GameManager.instance.AddScore(scoreReward);

        Destroy(healthBar); // Destroy health bar on death
        Destroy(gameObject); // Destroy the enemy
    }

    void EndGame()
    {
        // Call Game Over method from GameOverManager
        GameOverManager.instance.GameOver();

        // Optionally, destroy the enemy if needed
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        ShowHealthBar();
    }
}
