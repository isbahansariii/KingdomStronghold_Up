//using UnityEngine;

//public class TowerBullet : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    public float force;
//    public int damage = 10; // Amount of damage the bullet will cause

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();

//        // Assuming the target is set when the bullet is instantiated
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            Vector3 direction = player.transform.position - transform.position;
//            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

//            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
//            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            // Get the PlayerHealth component from the enemy
//            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
//            if (playerHealth != null)
//            {
//                // Apply damage to the enemy
//                playerHealth.TakeDamage(damage);
//            }

//            // Destroy the bullet after it hits the player
//            Destroy(gameObject);
//        }
//    }

//    void Update()
//    {
//        // Optional: Destroy the bullet after a certain time to prevent it from existing indefinitely
//        Destroy(gameObject, 3f); // Destroy after 3 seconds if no collision
//    }
//}

//-------------------------------------------------------XXXX-----------------------------------------

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
