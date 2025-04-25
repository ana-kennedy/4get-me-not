using UnityEngine;
using System.Collections;

public class GhostTimer : MonoBehaviour
{
    public Animator GhostTimerTexture;
    public GameObject GhostTimerCollision;
    public bool InRange = false;
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;

    void Start()
    {
        StartCoroutine(TimerCycle());
    }

    void Update()
    {
        if (!InRange) // Same as InRange == false
        {
            GhostTimerCollision.SetActive(true);
            GhostTimerTexture.SetBool("Active", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            InRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) // ✅ Detect when Ball leaves
    {
        if (other.CompareTag("Ball"))
        {
            InRange = false;
            audioSource.Stop(); // ✅ Stop any currently playing audio when leaving
            Debug.Log("Ball left the Ghost Timer area. Audio stopped.");
        }
    }

    private IEnumerator TimerCycle()
    {
        while (true) // Infinite loop to keep toggling
        {
            yield return new WaitForSeconds(0.7f);
            GhostTimerCollision.SetActive(false);
            GhostTimerTexture.SetBool("Active", true);
            
            if (InRange)
            {
                audioSource.PlayOneShot(clip1, 0.05f);
                Debug.Log("Playing Clip 1");
            }

            yield return new WaitForSeconds(0.7f);
            GhostTimerCollision.SetActive(true);
            GhostTimerTexture.SetBool("Active", false);
            
            if (InRange)
            {
                audioSource.PlayOneShot(clip2, 0.05f);
                Debug.Log("Playing Clip 2");
            }
        }
    }
}