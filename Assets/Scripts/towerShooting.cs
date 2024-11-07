//using System.Collections.Generic;
//using UnityEngine;

//public class towerShooting : MonoBehaviour
//{
//    public GameObject bullet;
//    public Transform bulletPos;
//    public float shootingInterval = 1f; // Time between shots
//    public float detectionRange = 2f; // Range within which the tower will shoot

//    private float timer;
//    private List<GameObject> enemiesInRange = new List<GameObject>();



//    void Update()
//    {
//        // Remove null enemies from the list (e.g., those that have been destroyed)
//        enemiesInRange.RemoveAll(enemy => enemy == null);

//        if (enemiesInRange.Count > 0)
//        {
//            timer += Time.deltaTime;
//            if (timer > shootingInterval)
//            {
//                timer = 0;
//                ShootAtClosestEnemy();
//            }
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            // Calculate the distance using the distance formula
//            float dx = other.transform.position.x - transform.position.x;
//            float dy = other.transform.position.y - transform.position.y;

//            float distance = Mathf.Sqrt(dx * dx + dy * dy); 
//            // √((x2 - x1)^2 + (y2 - y1)^2)

//            // Add the player to the list if they are within detection range
//            if (distance <= detectionRange && !enemiesInRange.Contains(other.gameObject))
//            {
//                enemiesInRange.Add(other.gameObject);
//            }
//        }
//    }

//    void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            // Remove the player from the list when they leave the detection range
//            enemiesInRange.Remove(other.gameObject);
//        }
//    }

//    void ShootAtClosestEnemy()
//    {
//        if (enemiesInRange.Count > 0)
//        {
//            // Find the closest enemy using manual distance calculations
//            GameObject closestEnemy = null;
//            float closestDistance = Mathf.Infinity;

//            foreach (GameObject enemy in enemiesInRange)
//            {
//                // Calculate distance to each enemy using the distance formula
//                float dx = enemy.transform.position.x - transform.position.x;
//                float dy = enemy.transform.position.y - transform.position.y;

//                float distance = Mathf.Sqrt(dx * dx + dy * dy); 
//                // √((x2 - x1)^2 + (y2 - y_1)^2)

//                if (distance < closestDistance)
//                {
//                    closestDistance = distance;
//                    closestEnemy = enemy;
//                }
//            }

//            // Shoot at the closest enemy if one was found within range
//            if (closestEnemy != null && closestDistance <= detectionRange)
//            {
//                Vector3 direction = (closestEnemy.transform.position - bulletPos.position).normalized;
//                Instantiate(bullet, bulletPos.position, Quaternion.LookRotation(direction));
//            }
//        }
//    }
//}


//--------------------------------------------XXXX-------------------------------------------

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
