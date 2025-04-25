using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BossTeaserScript : MonoBehaviour
{

    public GameObject Bushman;
    public GameObject BushmanUI;
    public GameObject UIBackground;
    public GameObject Camera;
    public GameObject Soundtrack;
    public GameObject GolfUI;
    public GameObject DialogueBox;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip bossAudio;
    public AudioClip bossAudio2;
    public bool hasTriggered = false;

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered)
        {
            hasTriggered = true;

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

            // Disable the AudioSource component on the "Soundtrack" GameObject
            if (Soundtrack != null)
            {
                AudioSource soundtrackAudio = Soundtrack.GetComponent<AudioSource>();
                if (soundtrackAudio != null)
                {
                    soundtrackAudio.enabled = false; // Disable AudioSource
                }
            }

            audioSource.PlayOneShot(bossAudio, 0.7f);
            Bushman.SetActive(true);
            BushmanUI.SetActive(true);
            UIBackground.SetActive(true);
            GolfUI.SetActive(false);
            StartCoroutine(BushmanSequenceOff(8f));
        }
    }

    private IEnumerator BushmanSequenceOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        BushmanUI.SetActive(false);
        UIBackground.SetActive(false);
        audioSource.Stop();
        StartDialogue();
    }

    private void StartDialogue()
    {
        DialogueBox.SetActive(true);
        audioSource2.PlayOneShot(bossAudio2, 0.3f);

        if (Camera != null)
            {
                // Disable CameraFollowObject
                CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null)
                {
                    followScript.enabled = false;
                }

                // Enable CameraFocusObject
                CameraFocusObject6 focusScript = Camera.GetComponent<CameraFocusObject6>();
                if (focusScript != null)
                {
                    focusScript.enabled = true;
                }
            }

        
    }
}
