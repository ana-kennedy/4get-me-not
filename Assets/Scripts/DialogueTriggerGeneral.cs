using System.Collections;
using UnityEngine;

public class DialogueTriggerGeneral : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject DialogueBox;

    [Header("Camera Settings")]
    public GameObject Camera;
    public MonoBehaviour[] cameraFocusOptions; // Assign all camera focus scripts here
    public int CameraFocusIndex; // Select which focus script to activate

    private bool hasTriggered = false;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered)
        {
            Debug.Log("Dialogue Trigger Activated! This will only trigger once.");
            DialogueBox.SetActive(true);
            hasTriggered = true;

            // Freeze Ball
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
                var followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null) followScript.enabled = false;

                int index = Mathf.Clamp(CameraFocusIndex, 0, cameraFocusOptions.Length - 1);
                if (cameraFocusOptions.Length > 0 && cameraFocusOptions[index] != null)
                {
                    cameraFocusOptions[index].enabled = true;
                }
                else
                {
                    Debug.LogWarning("Invalid or missing CameraFocus at index " + CameraFocusIndex);
                }
            }

            StartCoroutine(RevertChanges(ballRb, ballDash, 3f));
        }
    }

    private IEnumerator RevertChanges(Rigidbody2D ballRb, BallDash ballDash, float delay)
    {
        Debug.Log("RevertChanges Coroutine Started!");
        yield return new WaitForSeconds(delay);

        if (Camera != null)
        {
            var followScript = Camera.GetComponent<CameraFollowObject>();
            if (followScript != null) followScript.enabled = true;

            int index = Mathf.Clamp(CameraFocusIndex, 0, cameraFocusOptions.Length - 1);
            if (cameraFocusOptions.Length > 0 && cameraFocusOptions[index] != null)
            {
                cameraFocusOptions[index].enabled = false;
            }
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