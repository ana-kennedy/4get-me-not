using UnityEngine;

public class DoorDialogueNHotel : MonoBehaviour
{
    public GameObject DialogueBox;
    public bool hasTriggered = false;
    public Animator InteractButton;
    public bool PlayerIsClose = false;
    public AudioSource source;
    public AudioClip clip;

    void Update()
    {
        if (PlayerIsClose && !hasTriggered && Input.GetKeyDown(KeyCode.E))
        {
            DialogueBox.SetActive(true);
            source.PlayOneShot(clip, 0.5f);
            hasTriggered = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerIsClose = true;
        InteractButton.SetBool("Exit", false);
        InteractButton.SetBool("Enter", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerIsClose = false;
        InteractButton.SetBool("Exit", true);
        InteractButton.SetBool("Enter", false);
    }
}