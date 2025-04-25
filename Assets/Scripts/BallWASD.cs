using UnityEngine;

public class BallWASD : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Adjust speed in the Inspector

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
    }

    void Update()
    {
        // Get movement input from WASD or Arrow Keys
        movementInput.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        movementInput.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        movementInput.Normalize(); // Prevent faster diagonal movement
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        // Apply movement using Rigidbody2D physics
        rb.velocity = movementInput * moveSpeed;
    }
}