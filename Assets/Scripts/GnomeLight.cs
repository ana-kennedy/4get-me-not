using System.Collections;
using UnityEngine;

public class GnomeLight : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed of movement
    public float waitTime = 1f;   // Pause time at each destination
    public Vector2 minBounds = new Vector2(-5f, -5f); // Bottom-left boundary
    public Vector2 maxBounds = new Vector2(5f, 5f);   // Top-right boundary

    private Vector2 targetPosition;

    void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            // Pick a random position within the range
            targetPosition = new Vector2(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y)
            );

            // Move towards the target position
            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // Wait for the next frame
            }

            // Pause at the destination
            yield return new WaitForSeconds(waitTime);
        }
    }
}