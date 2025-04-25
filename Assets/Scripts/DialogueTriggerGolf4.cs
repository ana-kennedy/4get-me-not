using System.Collections;
using UnityEngine;

public class DialogueTriggerGolf4 : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject Camera;
    private bool hasTriggered = false;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered)
        {
            Debug.Log("Dialogue Trigger Activated! This will only trigger once.");
            DialogueBox.SetActive(true);
            hasTriggered = true; // ✅ Now this will NEVER be reset

            // Get Ball's Rigidbody
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            BallDash ballDash = other.GetComponent<BallDash>(); // Reference to player movement script

            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero; // Stop all movement
                ballRb.constraints = RigidbodyConstraints2D.FreezeAll; // Completely freeze the ball
                Debug.Log("Ball frozen!");

                if (ballDash != null)
                {
                    ballDash.isMovementDisabled = true; // Disable input if using BallDash script
                    Debug.Log("Ball input disabled!");
                }
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
            }

            // Start the coroutine but ensure it only happens once
            StartCoroutine(RevertChanges(ballRb, ballDash, 3f));
            Debug.Log("Started RevertChanges Coroutine.");
        }
    }

    [System.Obsolete]
    private IEnumerator RevertChanges(Rigidbody2D ballRb, BallDash ballDash, float delay)
    {
        Debug.Log("RevertChanges Coroutine Started!");
        yield return new WaitForSeconds(delay);
        Debug.Log("Reverting changes now!");

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
            ballRb.constraints = RigidbodyConstraints2D.None; // Unfreeze ball
            ballRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent unwanted rotation
            Debug.Log("Ball Unfrozen!");
        }

        if (ballDash != null)
        {
            ballDash.isMovementDisabled = false; // ✅ Ensure movement is enabled again
            Debug.Log("Ball movement re-enabled!");
        }

        // ✅ **DO NOT RESET `hasTriggered` HERE** (prevents retriggering)
        Debug.Log("RevertChanges Complete! This event will NEVER trigger again.");
    }
}