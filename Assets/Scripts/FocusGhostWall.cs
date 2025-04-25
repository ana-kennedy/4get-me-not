using UnityEngine;
using System.Collections;

public class FocusGhostWall : MonoBehaviour
{
private bool hasTriggered = false;
public GameObject Camera;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered)
        {
            hasTriggered = true;

            // Stop the Ball's movement
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero; // Stop movement
                ballRb.isKinematic = true; // Temporarily disable physics
            }

            if (Camera != null)
            {
                // Disable CameraFollowObject
                CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null)
                {
                    followScript.enabled = false;
                }

                // Enable CameraFocusObject
                CameraFocusObject4 focusScript = Camera.GetComponent<CameraFocusObject4>();
                if (focusScript != null)
                {
                    focusScript.enabled = true;
                }

                // Start Coroutine to revert after 3 seconds
                StartCoroutine(RevertChanges(ballRb, 1.5f));
            }
        }
    }

    [System.Obsolete]
    private IEnumerator RevertChanges(Rigidbody2D ballRb, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (Camera != null)
        {
            // Re-enable CameraFollowObject
            CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
            if (followScript != null)
            {
                followScript.enabled = true;
            }

            // Disable CameraFocusObject
            CameraFocusObject4 focusScript = Camera.GetComponent<CameraFocusObject4>();
            if (focusScript != null)
            {
                focusScript.enabled = false;
            }
        }

        // Allow the Ball to move again
        if (ballRb != null)
        {
            ballRb.isKinematic = false; // Re-enable physics
        }
    }
}

