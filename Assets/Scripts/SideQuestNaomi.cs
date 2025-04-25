using UnityEngine;

public class SideQuestNaomi : MonoBehaviour
{
    public GameObject DialogueBox;
    public AudioSource audioSource;
    public AudioClip interactFX;
    public bool playerisClose = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.Instance.GetEventState("NaomiTalked1") && playerisClose)
        {
            DialogueBox.SetActive(true);
            audioSource.PlayOneShot(interactFX, 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerisClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerisClose = false;
        }
    }
}
