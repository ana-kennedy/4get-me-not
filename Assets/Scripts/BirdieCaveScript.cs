using UnityEngine;

public class BirdieCaveScript : MonoBehaviour
{
    public GameObject InteractButton;
    public AudioSource source;
    public AudioClip clip;
    public bool PlayerIsClose;
    public GameObject BirdieDialogueBox;

     void Update()
    {


        if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
            BirdieDialogueBox.SetActive(true);
            source.PlayOneShot(clip);
        }
    }

       private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetActive(true);
            GetComponent<DialogueUI>();
            PlayerIsClose = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
            InteractButton.SetActive(false);
        }
    }
}
