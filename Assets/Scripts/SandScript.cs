using UnityEngine;

public class SandScript : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // Adjust to control how much the Ball slows down
    private float originalDrag; // Store the original drag value
    private bool hasEnteredSand = false; // ✅ Prevents multiple slowdowns

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasEnteredSand) // ✅ Prevent slowdown stacking
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                originalDrag = rb.drag; // Store original drag only once
                rb.drag = originalDrag * slowDownFactor; // Apply slowdown
                hasEnteredSand = true; // ✅ Mark that the effect was applied
                Debug.Log("Ball entered sand. Drag increased.");
            }
        }
    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && hasEnteredSand) // ✅ Ensure drag resets only if slowed down
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.drag = originalDrag; // Restore original drag
                hasEnteredSand = false; // ✅ Allow effect to reapply on next entry
                Debug.Log("Ball exited sand. Drag restored.");
            }
        }
    }
}