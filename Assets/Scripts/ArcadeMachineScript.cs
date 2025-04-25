using Unity.VisualScripting;
using UnityEngine;

public class ArcadeMachineScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fx;
    public bool playerIsClose = false;
    public Animator InteractButton;

    void Update()
    {
        if(playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(fx, 0.7f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = true;
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
      if(other.CompareTag("Player"))
      {
        playerIsClose = false;
        InteractButton.SetBool("Exit", true);
        InteractButton.SetBool("Enter", false);
      }
    }
}
