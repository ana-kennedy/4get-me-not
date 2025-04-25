using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class BaltoDialogueTrigger : MonoBehaviour
{

    public GameObject DialogueBox;
    public AudioSource audioSource;
    public AudioClip interactFX;
    public bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Balto") && !hasTriggered)
        {
            hasTriggered = true;
            DialogueBox.SetActive(true);
            BaltoRide baltoRide = other.GetComponent<BaltoRide>();
            if (baltoRide != null)
            {
                baltoRide.enabled = false;
            }
            Rigidbody2D baltoRb = other.GetComponent<Rigidbody2D>();
            if (baltoRb != null)
            {
                baltoRb.simulated = false;
            }
            audioSource.PlayOneShot(interactFX, 0.5f);
            StartCoroutine(RevertChanges(5f));
        }
    }

    private IEnumerator RevertChanges(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject balto = GameObject.FindGameObjectWithTag("Balto");
        if (balto != null)
        {
            BaltoRide baltoRide = balto.GetComponent<BaltoRide>();
            if (baltoRide != null)
            {
                baltoRide.enabled = true;
            }
            Rigidbody2D baltoRb = balto.GetComponent<Rigidbody2D>();
            if (baltoRb != null)
            {
                baltoRb.simulated = true;
            }
        }
    }

}
