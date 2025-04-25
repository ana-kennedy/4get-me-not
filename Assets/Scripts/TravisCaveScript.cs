using UnityEngine;

public class TravisCaveScript : MonoBehaviour

{

    public Animator InteractButton;
    public AudioSource source;
    public AudioClip clip;
    public bool PlayerIsClose;
    public GameObject DialogueBox;
    public GameObject DialogueBox2;
    public bool hasInteracted = false;

    void Start()
    {
     InteractButton.SetBool("Default", true);   
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose && !hasInteracted)
        {
            hasInteracted = true;
            source.PlayOneShot(clip, 0.5f);

        if(DialogueUIBirdie.hasTalked)
        {
            DialogueBox2.SetActive(true);
        }
        else
        {
            DialogueBox.SetActive(true);
        }

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
            GetComponent<DialogueUI>();
            PlayerIsClose = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetBool("Exit", true);
            InteractButton.SetBool("Enter", false);
            PlayerIsClose = false;
        }
    }
}