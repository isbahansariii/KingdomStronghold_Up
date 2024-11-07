
using System.Collections.Generic;
using UnityEngine;

public class towerShooting : MonoBehaviour
{
    public GameObject bullet; // Prefab of the bullet (stone)
    public Transform bulletPos; // Position from where the bullet is fired
    public float shootingInterval = 1f; // Time between shots
    public float detectionRange = 2f; // Range within which the tower will shoot

    private float timer;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    void Update()
    {
        UpdateEnemiesInRange();

        if (enemiesInRange.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer > shootingInterval)
            {
                timer = 0;
                ShootAtClosestEnemy();
            }
        }
    }

    void UpdateEnemiesInRange()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Player");

        // Clear the list and check for enemies within detection range
        enemiesInRange.Clear();
        foreach (var enemy in allEnemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= detectionRange)
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }

    void ShootAtClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            // Calculate direction to shoot at the closest enemy
            Vector3 direction = (closestEnemy.transform.position - bulletPos.position).normalized;
            GameObject newBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);

            // Set the direction for the bullet
            TowerBullet towerBullet = newBullet.GetComponent<TowerBullet>();
            if (towerBullet != null)
            {
                towerBullet.SetDirection(direction);
            }
        }
    }
}
