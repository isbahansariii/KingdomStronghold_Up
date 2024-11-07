
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public int damage = 10; // Damage dealt by the bullet
    private Vector3 direction; // Direction in which bullet will move

    // Method to set direction from tower
    public void SetDirection(Vector3 shootDirection)
    {
        direction = shootDirection;
        // Rotate the bullet to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust rotation if necessary
    }

    void Update()
    {
        // Move bullet in the set direction
        transform.position += direction * speed * Time.deltaTime;

        // Destroy bullet after a certain time if it doesn’t hit anything
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy bullet after it hits
        }
    }
}
