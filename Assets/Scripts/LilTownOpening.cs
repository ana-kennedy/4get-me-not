using System.Collections;
using UnityEngine;

public class LilTownOpening : MonoBehaviour
{
public GameObject Logo;
public GameObject MovementSheet;

public AudioSource audioSource;
public AudioClip sparkle;
public bool hasTriggered = false;



void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player") && !GameManager.Instance.GetEventState("opening_triggered") && !hasTriggered)
    {
        hasTriggered = true;
        audioSource.PlayOneShot(sparkle);
        Logo.SetActive(true);
        MovementSheet.SetActive(true);
        StartCoroutine(TimedLogo(3f));
        GameManager.Instance.SetEventState("opening_triggered", true);
        Debug.Log("Opening Triggered! Event saved in GameManager.");
        QuestManager.Instance.AddQuest("TALK TO CHER");
    }
}

    private IEnumerator TimedLogo(float delay)
    {
        yield return new WaitForSeconds(delay);
        Logo.SetActive(false);
    }
}
