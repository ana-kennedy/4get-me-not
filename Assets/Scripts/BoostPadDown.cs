using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public float boostForce = 10f; // Adjustable downward force
    private float originalYVelocity; // Stores original velocity before boost
    public GameObject PostProcessing;
    public AudioSource audioSource;
    public AudioClip clip1;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                // Store original velocity
                originalYVelocity = ballRb.velocity.y;

                // Apply downward force
                ballRb.velocity = new Vector2(ballRb.velocity.x, -boostForce);
            }
        }

        PostProcessing.SetActive(true);
        audioSource.PlayOneShot(clip1);
    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                // Restore original velocity
                ballRb.velocity = new Vector2(ballRb.velocity.x, originalYVelocity);
            }
        }
        PostProcessing.SetActive(false);
    }
}