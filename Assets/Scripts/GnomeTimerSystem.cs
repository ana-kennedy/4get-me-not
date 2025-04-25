using UnityEngine;
using System.Collections;

public class GnomeTimerSystem : MonoBehaviour
{
    [Header("Teleport & Countdown")]
    public GameObject teleportPoint;
    public Animator teleportAnimator;
    public GameObject timerCollider;
    public GameObject timerTextObject;
    private Animator timerTextAnimator;
    public GameObject timerCountdown;

    [Header("Timer Door")]
    public Animator timerDoorTextureAnimator; // New animator reference

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip countdownClip;
    public AudioClip activationClip;

    [Header("Camera")]
    public GameObject cameraObject;

    private Component cameraFocus;
    private Component cameraFollow;
    public bool hasTriggered = false;

    private void Awake()
    {
        // Cache animator if it exists
        if (timerTextObject != null)
        {
            timerTextAnimator = timerTextObject.GetComponent<Animator>();
        }

        // Cache camera components
        if (cameraObject != null)
        {
            cameraFocus = cameraObject.GetComponent("CameraFocusObject");
            cameraFollow = cameraObject.GetComponent("CameraFollowObject");

            if (cameraFocus == null || cameraFollow == null)
            {
                Debug.LogWarning("Missing CameraFocusObject or CameraFollowObject on Camera.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball") || hasTriggered) return;

        teleportAnimator.SetBool("Active", true);
        hasTriggered = true;
        audioSource.PlayOneShot(activationClip, 0.5f);

        // Trigger Timer Door Animation
        if (timerDoorTextureAnimator != null)
        {
            timerDoorTextureAnimator.SetBool("Active", true);
            timerDoorTextureAnimator.SetBool("Inactive", false);
        }

        // Enable camera focus
        if (cameraFocus != null && cameraFollow != null)
        {
            ((Behaviour)cameraFocus).enabled = true;
            ((Behaviour)cameraFollow).enabled = false;
        }

        Invoke(nameof(StartCameraSequence), 3f);
    }

    private void StartCameraSequence()
    {
        // Revert camera BEFORE starting the coroutine
        if (cameraFocus != null && cameraFollow != null)
        {
            ((Behaviour)cameraFocus).enabled = false;
            ((Behaviour)cameraFollow).enabled = true;
        }


        StartCoroutine(TimerSequence());
    }

    private IEnumerator TimerSequence()
    {
        timerCountdown.SetActive(true);
        timerTextObject.SetActive(true);

        if (timerTextAnimator != null)
        {
            timerTextAnimator.Play("CountdownAnimation", -1, 0f); // Replace with your actual animation name
        }

        timerCollider.SetActive(false);
        PlayCountdownSound();

        yield return new WaitForSeconds(10f);

        timerTextObject.SetActive(false);
        timerCountdown.SetActive(false);
        timerCollider.SetActive(true);
        teleportAnimator.SetBool("Inactive", true);
        
        if (timerDoorTextureAnimator != null)
        {
            timerDoorTextureAnimator.SetBool("Active", false);
            timerDoorTextureAnimator.SetBool("Inactive", true);
        }

        

        hasTriggered = false;
    }

    private void PlayCountdownSound()
    {
        if (audioSource != null && countdownClip != null)
        {
            audioSource.PlayOneShot(countdownClip, 0.5f);
        }
    }
}