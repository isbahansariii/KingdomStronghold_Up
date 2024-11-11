///*
//- Manual Range Detection: Uses custom distance calculations to detect enemies within range.
//- Targeting Closest Enemy: Finds and targets the closest enemy in range.
//- Timed Shooting: Fires bullets at intervals toward the detected enemy.
//- Directional Bullets: Sets each bullet’s direction toward its target manually.
//*/


//using System.Collections.Generic;
//using UnityEngine;

//public class towerShooting : MonoBehaviour
//{
//    public GameObject bullet; // Prefab of the bullet (stone) to be shot at enemies
//    public Transform bulletPos; // Position from where the bullet is fired (e.g., the top of the tower)
//    public float shootingInterval = 1f; // Time interval between each shot
//    public float detectionRange = 2f; // Range within which the tower detects and targets enemies

//    private float timer; // Timer to keep track of shooting intervals
//    private List<GameObject> enemiesInRange = new List<GameObject>(); // List of enemies within the detection range

//    void Update()
//    {
//        UpdateEnemiesInRange(); // Continuously update the list of enemies within range

//        // Check if there are any enemies in range and if the tower is ready to shoot
//        if (enemiesInRange.Count > 0)
//        {
//            timer += Time.deltaTime; // Increment timer by time elapsed since last frame
//            if (timer > shootingInterval) // Check if the time interval has passed
//            {
//                timer = 0; // Reset the timer
//                ShootAtClosestEnemy(); // Call method to shoot at the closest enemy
//            }
//        }
//    }

//    void UpdateEnemiesInRange()
//    {
//        // Find all GameObjects with the "Player" tag (assumed to be enemies)
//        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Player");

//        enemiesInRange.Clear(); // Clear the previous list of enemies in range

//        // Check each enemy to see if they are within detection range
//        foreach (var enemy in allEnemies)
//        {
//            if (enemy != null) // Ensure the enemy still exists
//            {
//                float distance = CalculateDistance(transform.position, enemy.transform.position); // Calculate manual distance
//                if (distance <= detectionRange) // Check if within range
//                {
//                    enemiesInRange.Add(enemy); // Add enemy to the list if within range
//                }
//            }
//        }
//    }

//    float CalculateDistance(Vector3 pos1, Vector3 pos2)
//    {
//        // Manual calculation of the distance between two points
//        float dx = pos2.x - pos1.x; // Difference in x-coordinates
//        float dy = pos2.y - pos1.y; // Difference in y-coordinates
//        return Mathf.Sqrt(dx * dx + dy * dy); // Return the square root of sum of squared differences
//    }

//    void ShootAtClosestEnemy()
//    {
//        GameObject closestEnemy = null; // Initialize variable to hold closest enemy
//        float closestDistance = Mathf.Infinity; // Start with an infinitely large distance

//        // Loop through each enemy in range to find the closest one
//        foreach (GameObject enemy in enemiesInRange)
//        {
//            float distance = CalculateDistance(transform.position, enemy.transform.position); // Manual distance calculation
//            if (distance < closestDistance) // Check if this enemy is closer than previous ones
//            {
//                closestDistance = distance; // Update closest distance
//                closestEnemy = enemy; // Update closest enemy
//            }
//        }

//        // Shoot a bullet toward the closest enemy
//        if (closestEnemy != null)
//        {
//            Vector3 direction = (closestEnemy.transform.position - bulletPos.position).normalized; // Calculate direction to closest enemy
//            GameObject newBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity); // Instantiate a new bullet at the bullet position

//            // Set the bullet’s direction if it has a TowerBullet component
//            TowerBullet towerBullet = newBullet.GetComponent<TowerBullet>();
//            if (towerBullet != null)
//            {
//                towerBullet.SetDirection(direction); // Set bullet to move in calculated direction
//            }
//        }
//    }
//}


using System.Collections.Generic;
using UnityEngine;

public class towerShooting : MonoBehaviour
{
    public GameObject bullet; // Prefab of the bullet (stone) to be shot at enemies
    public Transform bulletPos; // Position from where the bullet is fired (e.g., the top of the tower)
    public float shootingInterval = 1f; // Time interval between each shot
    public float detectionRange = 2f; // Range within which the tower detects and targets enemies
    private float timer; // Timer to keep track of shooting intervals
    private List<GameObject> enemiesInRange = new List<GameObject>(); // List of enemies within the detection range

    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Check if the AudioSource component exists, if not, add it dynamically
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource dynamically if not present
        }

        // Optionally, you can set up some default settings for the AudioSource if needed:
        // audioSource.loop = false; // If you don't want the sound to loop
        // audioSource.playOnAwake = false; // Prevent it from playing automatically
    }

    void Update()
    {
        UpdateEnemiesInRange(); // Continuously update the list of enemies within range

        // Check if there are any enemies in range and if the tower is ready to shoot
        if (enemiesInRange.Count > 0)
        {
            timer += Time.deltaTime; // Increment timer by time elapsed since last frame
            if (timer > shootingInterval) // Check if the time interval has passed
            {
                timer = 0; // Reset the timer
                ShootAtClosestEnemy(); // Call method to shoot at the closest enemy
            }
        }
    }

    void UpdateEnemiesInRange()
    {
        // Find all GameObjects with the "Player" tag (assumed to be enemies)
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Player");

        enemiesInRange.Clear(); // Clear the previous list of enemies in range

        // Check each enemy to see if they are within detection range
        foreach (var enemy in allEnemies)
        {
            if (enemy != null) // Ensure the enemy still exists
            {
                float distance = CalculateDistance(transform.position, enemy.transform.position); // Calculate manual distance
                if (distance <= detectionRange) // Check if within range
                {
                    enemiesInRange.Add(enemy); // Add enemy to the list if within range
                }
            }
        }
    }

    float CalculateDistance(Vector3 pos1, Vector3 pos2)
    {
        // Manual calculation of the distance between two points
        float dx = pos2.x - pos1.x; // Difference in x-coordinates
        float dy = pos2.y - pos1.y; // Difference in y-coordinates
        return Mathf.Sqrt(dx * dx + dy * dy); // Return the square root of sum of squared differences
    }

    void ShootAtClosestEnemy()
    {
        GameObject closestEnemy = null; // Initialize variable to hold closest enemy
        float closestDistance = Mathf.Infinity; // Start with an infinitely large distance

        // Loop through each enemy in range to find the closest one
        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = CalculateDistance(transform.position, enemy.transform.position); // Manual distance calculation
            if (distance < closestDistance) // Check if this enemy is closer than previous ones
            {
                closestDistance = distance; // Update closest distance
                closestEnemy = enemy; // Update closest enemy
            }
        }

        // Shoot a bullet toward the closest enemy
        if (closestEnemy != null)
        {
            Vector3 direction = (closestEnemy.transform.position - bulletPos.position).normalized; // Calculate direction to closest enemy
            GameObject newBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity); // Instantiate a new bullet at the bullet position

            // Set the bullet’s direction if it has a TowerBullet component
            TowerBullet towerBullet = newBullet.GetComponent<TowerBullet>();
            if (towerBullet != null)
            {
                towerBullet.SetDirection(direction); // Set bullet to move in calculated direction
            }

            // Play the shot sound
            audioSource.PlayOneShot(Resources.Load<AudioClip>("shot")); // Assuming the sound file is placed in Assets/Resources/shot.mp3
        }
    }
}

