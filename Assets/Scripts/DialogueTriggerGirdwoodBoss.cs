using System.Collections;
using UnityEngine;

public class DialogueTriggerGirdwoodBoss : MonoBehaviour
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
            hasTriggered = true;

            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            BallDash ballDash = other.GetComponent<BallDash>();

            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero;
                ballRb.constraints = RigidbodyConstraints2D.FreezeAll;
                Debug.Log("Ball frozen!");

                if (ballDash != null)
                {
                    ballDash.isMovementDisabled = true;
                    Debug.Log("Ball input disabled!");
                }
            }

            if (Camera != null)
            {
                CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null) followScript.enabled = false;

                CameraFocusObject focusScript = Camera.GetComponent<CameraFocusObject>();
                if (focusScript != null) focusScript.enabled = true;
            }

            StartCoroutine(RevertChanges(ballRb, ballDash, 2f));
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
            CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
            if (followScript != null) followScript.enabled = true;

            CameraFocusObject focusScript = Camera.GetComponent<CameraFocusObject>();
            if (focusScript != null) focusScript.enabled = false;
        }

        if (ballRb != null)
        {
            ballRb.constraints = RigidbodyConstraints2D.None;
            ballRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Debug.Log("Ball Unfrozen!");
        }

        if (ballDash != null)
        {
            ballDash.isMovementDisabled = false;
            Debug.Log("Ball movement re-enabled!");
        }

        Debug.Log("RevertChanges Complete! This event will NEVER trigger again.");
    }
}