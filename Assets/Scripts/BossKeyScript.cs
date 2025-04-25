using UnityEngine;
using System.Collections;

public class BossKeyScript : MonoBehaviour
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
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip1, 0.3f);
            }
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

            if (KeyDoorCollision != null)
                KeyDoorCollision.SetActive(false);

            if (KeyDoorAni != null)
                KeyDoorAni.SetBool("Active", true);

            if (audioSource != null)
                audioSource.PlayOneShot(clip2, 0.3f);

            if (Key != null)
                Key.SetActive(false);
        }
    }
}