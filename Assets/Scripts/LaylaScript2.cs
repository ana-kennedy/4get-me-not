using UnityEngine;

public class LaylaScript2 : MonoBehaviour

{

    public GameObject InteractButton;
    public AudioSource source;
    public AudioClip clip;
    public bool PlayerIsClose;
    public GameObject LaylaDialogueBox;

    void Update()
    {


        if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
            LaylaDialogueBox.SetActive(true);
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