using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    // Array of enemy prefabs for different types of enemies
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;   // Spawn locations for enemies
    public TextMeshProUGUI waveText;  // TextMeshProUGUI for displaying wave number
    public Button waveButton;         // Button to start the next wave

    public int enemiesPerWave = 5;    // Number of enemies per wave
    private int currentWave = 1;      // Track the current wave number
    private int enemiesAlive = 0;     // Track how many enemies are currently alive

    void Start()
    {
        // Set button listener and disable button at the start
        waveButton.onClick.AddListener(StartWave);
        waveButton.interactable = true;
        waveText.text = "Wave: " + currentWave;
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        waveButton.interactable = false;  // Disable the button until the wave is complete

        // Spawn enemies based on the current wave
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // Choose a random enemy prefab from the array of enemyPrefabs
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Choose a random spawn point from the available spawn points
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the enemy at the chosen spawn point
            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Increment the enemies alive count
            enemiesAlive++;
            yield return new WaitForSeconds(1f);  // Delay between each enemy spawn
        }
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            // Prepare for the next wave when all enemies are defeated
            currentWave++;
            enemiesPerWave += 2;  // Increase the number of enemies in the next wave
            waveText.text = "Wave: " + currentWave;  // Update wave number in TextMeshPro
            waveButton.interactable = true;  // Re-enable the button for the next wave
        }
    }
}
