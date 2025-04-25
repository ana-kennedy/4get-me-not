using UnityEngine;

public class MoverAndFollower : MonoBehaviour
{
    [Header("References")]
    public Transform mover;       // The object that moves
    public Transform follower;    // The object that follows

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float minX = -5f;      // Minimum X position
    public float maxX = 5f;       // Maximum X position

    private bool movingRight = true;

    void Update()
    {
        // Determine direction
        if (movingRight)
        {
            mover.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (mover.position.x >= maxX)
                movingRight = false;
        }
        else
        {
            mover.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (mover.position.x <= minX)
                movingRight = true;
        }

        // Set follower to exactly match mover's position
        follower.position = mover.position;
    }
}