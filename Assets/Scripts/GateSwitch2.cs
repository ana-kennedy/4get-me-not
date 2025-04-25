using UnityEngine;
using System.Collections;

public class GateSwitch2 : MonoBehaviour
{
    public Animator GateButtonTexture;
    public Animator GateTexture;
    public GameObject GateWallCollision;
    public GameObject Camera;
    private bool hasTriggered = false;
    public AudioSource audioSource;
    public AudioClip clip;

    void Update()
    {
        if (hasTriggered)
        {
            GateWallCollision.SetActive(false);
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered) // Ensures trigger happens only once
        {
            hasTriggered = true;
            GateTexture.SetBool("Active", true);
            GateButtonTexture.SetBool("Active", true);
            CollectSound();

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
                CameraFocusObject5 focusScript = Camera.GetComponent<CameraFocusObject5>();
                if (focusScript != null)
                {
                    focusScript.enabled = true;
                }

                // Start Coroutine to revert after 3 seconds
                StartCoroutine(RevertChanges(ballRb, 3f));
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
            CameraFocusObject5 focusScript = Camera.GetComponent<CameraFocusObject5>();
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

    void CollectSound()
    {
        if (audioSource != null && clip != null && !audioSource.isPlaying) // Ensures it plays only once
        {
            audioSource.PlayOneShot(clip, 0.3f);
        }
    }
}