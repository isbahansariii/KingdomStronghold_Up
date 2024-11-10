/*
- Manual Collision Detection: Uses custom distance calculation to detect collisions with enemies.
- Bullet Direction & Movement: Sets bullet direction and moves it toward the target.
- Damage Application: Inflicts damage to an enemy upon collision and destroys the bullet afterward. 
*/

using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public int damage = 10; // Damage dealt by the bullet
    private Vector3 direction; // Direction in which bullet will move

    private float collisionRange = 0.5f; // Custom collision detection range

    void Update()
    {
        // Move bullet in the set direction based on speed and time
        transform.position += direction * speed * Time.deltaTime;

        // Manually check for collisions with enemies
        CheckCollisionWithEnemies();

        // Destroy bullet after a set time if it doesn’t hit anything
        Destroy(gameObject, 3f);
    }

    // Sets the direction for the bullet
    public void SetDirection(Vector3 shootDirection)
    {
        direction = shootDirection; // Set the movement direction of the bullet
        // Calculate the angle for rotation to face the direction of travel
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Rotate bullet to face the calculated angle
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    // Manually checks for collisions with enemies
    void CheckCollisionWithEnemies()
    {
        // Get all objects tagged as "Player" (enemies)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                // Calculate distance between bullet and each enemy
                float distanceToEnemy = CalculateDistance(transform.position, enemy.transform.position);

                // Check if distance is within collision range
                if (distanceToEnemy <= collisionRange)
                {
                    // Get enemy's health component and apply damage
                    PlayerHealth playerHealth = enemy.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                    }
                    Destroy(gameObject); // Destroy bullet after hitting enemy
                    return; // Exit loop after collision
                }
            }
        }
    }

    // Manually calculates distance between two points
    float CalculateDistance(Vector3 pos1, Vector3 pos2)
    {
        float dx = pos2.x - pos1.x; // Difference in x
        float dy = pos2.y - pos1.y; // Difference in y
        return Mathf.Sqrt(dx * dx + dy * dy); // Pythagorean theorem to get distance
    }
}

