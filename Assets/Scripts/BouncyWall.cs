using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class BounceEffect : MonoBehaviour
{


    public float bounceForce = 10f; // Adjust this for a stronger or weaker bounce

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                // Calculate the bounce direction (reflecting velocity off the surface normal)
                Vector2 normal = collision.contacts[0].normal; // Get the collision normal
                Vector2 bounceDirection = Vector2.Reflect(ballRb.velocity.normalized, normal);

                // Apply force in the bounce direction
                ballRb.velocity = Vector2.zero; // Reset current velocity
                ballRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);


                Debug.Log("Ball bounced! New direction: " + bounceDirection);
            }
        }
    }
}