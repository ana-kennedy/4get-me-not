using System.Collections;
using UnityEngine;

public class TimerBoxScript1 : MonoBehaviour
{
    public GameObject TimerBoxes;
    public bool InRange = false;
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    
    public Transform target; // Assign the player or relevant object in the Inspector

    private void Start()
    {
        StartCoroutine(TimerCycle());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            InRange = true;
        }
        else {
            InRange = false;
        }
    }

    private IEnumerator TimerCycle()
    {
        while (true) // Infinite loop to keep toggling
        {
            yield return new WaitForSeconds(1f);
            TimerBoxes.SetActive(false);
            if (InRange == true)
            {
                audioSource.PlayOneShot(clip1, 0.3f);
            }

            yield return new WaitForSeconds(1f);
            TimerBoxes.SetActive(true);
            if (InRange == true)
            {
                audioSource.PlayOneShot(clip2, 0.3f);
            }
        }
    }
}