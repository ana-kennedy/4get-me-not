using UnityEngine;

public class TorchScript : MonoBehaviour
{
    public GameObject torch; // Assign your Torch GameObject in the Inspector
    public GameObject torchLighting; // Assign your Torch Lighting GameObject
    public float followSpeed = 5f; // Adjust speed of following
    private Transform ballTransform;
    private bool isFollowing = false;
    private bool hasPlayedSound = false; // New flag to track sound playback

    public AudioSource audioSource;
    public AudioClip clip1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            ballTransform = other.transform;
            isFollowing = true;

            if (!hasPlayedSound) // Play sound only if it hasn't been played
            {
                audioSource.PlayOneShot(clip1, 0.3f);
                hasPlayedSound = true; // Set flag to true after playing
            }
        }
    }

    private void Update()
    {
        if (isFollowing && ballTransform != null)
        {
            // Smoothly move the Torch and Torch Lighting towards the Ball
            torch.transform.position = Vector3.Lerp(torch.transform.position, ballTransform.position, followSpeed * Time.deltaTime);
            torchLighting.transform.position = Vector3.Lerp(torchLighting.transform.position, ballTransform.position, followSpeed * Time.deltaTime);
        }
    }
}