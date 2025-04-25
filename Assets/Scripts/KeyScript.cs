using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    public Animator KeyDoorAni;
    public GameObject KeyDoorCollision;
    public GameObject Key;
    private bool hasUsedDoor = false;
    public float followSpeed = 3f;
    private Transform ballTransform;
    private bool isFollowing = false;
    private bool hasTriggered = false;
    private Vector3 offset = new Vector3(1f, 1f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered)
        {
            hasTriggered = true;
            ballTransform = other.transform;
            isFollowing = true;
            audioSource.PlayOneShot(clip1, 0.3f);
        }
    }



    private void Update()
    {
        if (isFollowing && ballTransform != null)
        {
            // Move smoothly towards the ball's position + offset
            transform.position = Vector3.Lerp(transform.position, ballTransform.position + offset, followSpeed * Time.deltaTime);
        }
    }

    public void UseKey()
    {
        if (!hasUsedDoor)
        {
            hasUsedDoor = true;
            KeyDoorCollision.SetActive(false);
            KeyDoorAni.SetBool("Active", true);
            audioSource.PlayOneShot(clip2, 0.3f); // Optional: play a second sound when door opens
            Key.SetActive(false);
        }
    }
}