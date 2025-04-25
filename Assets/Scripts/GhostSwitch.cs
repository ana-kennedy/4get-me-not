using UnityEngine;

public class GhostSwitch : MonoBehaviour
{
    public Animator GhostAnimation;
    public Animator Pressed;
    public GameObject GhostWallCollision;
    private bool hasTriggered = false;
    public AudioSource audioSource;
    public AudioClip clip;

    void Update()
    {
        if (hasTriggered)
        {
            GhostWallCollision.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered) // Ensures trigger happens only once
        {
            hasTriggered = true;
            GhostAnimation.SetBool("Active", true);
            Pressed.SetBool("Active", true);
            CollectSound();
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