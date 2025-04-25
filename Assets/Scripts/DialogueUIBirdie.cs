using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class DialogueUIBirdie : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;
    public AudioClip  clip2;
    public GameObject Bogey;
    public GameObject DoubleBogey;
    public GameObject TripleBogey;
    static public bool hasTalked = false;



    private typewriterEffect typewriterEffect;



    private void Start()
    {
      typewriterEffect = GetComponent<typewriterEffect>();
      ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;    
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

  private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
{
    for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
    {
        string dialogue = dialogueObject.Dialogue[i];

        yield return typewriterEffect.Run(dialogue, textLabel);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        // Dialogue FX Sound
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            source.PlayOneShot(clip, 0.5f);
        }

        // Trigger custom logic for specific dialogue elements
        if (i == 17 || i == 18 || i == 19)
        {
            if (i == 17)
            {
                Bogey.SetActive(true);
                source.PlayOneShot(clip2, 0.1f);
            }
            if (i == 18)
            {
                DoubleBogey.SetActive(true);
                source.PlayOneShot(clip2, 0.1f);
            }
            if (i ==19)
            {
                TripleBogey.SetActive(true);
                source.PlayOneShot(clip2, 0.1f);
            }
        }
    }

    CloseDialogueBox();
}

    private void CloseDialogueBox()
    {
        hasTalked = true;
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }

}
