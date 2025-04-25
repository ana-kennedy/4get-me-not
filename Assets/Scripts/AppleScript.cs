using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public float speed = 5f; // Base speed of the apple
    public float turnSpeed = 200f; // How fast the apple turns toward the player
    public GameObject ball; // Reference to the Ball GameObject

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component

        // Find the Ball GameObject if not assigned in Inspector
        if (ball == null)
        {
            ball = GameObject.FindWithTag("Ball"); // Ensure your Ball GameObject has the tag "Ball"
        }

        if (ball == null)
        {
            Debug.LogWarning("Ball GameObject not found! Make sure it's tagged as 'Ball'.");
            Destroy(gameObject); // Destroy projectile if no target is found
        }
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        if (ball == null) return;

        // Get direction towards the Ball
        Vector2 direction = (ball.transform.position - transform.position).normalized;

        // Rotate smoothly towards the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);

        // Move the apple forward in the new direction
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the apple on any collision
        Destroy(gameObject);
    }
}