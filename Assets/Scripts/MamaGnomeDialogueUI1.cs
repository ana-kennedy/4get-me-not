using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class MamaGnomeDialogueUI1 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private MamaGnomeSequence sequenceManager;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;

    private typewriterEffect typewriterEffect;

   private void Start()
{
    typewriterEffect = GetComponent<typewriterEffect>();

    if (sequenceManager == null)
    {
        sequenceManager = GameObject.Find("MamaGnomeSequence").GetComponent<MamaGnomeSequence>();
    }

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

            // Optional: Dialogue FX Sound
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.PlayOneShot(clip, 0.3f);
            }

            
            switch (i)
            {
                case 2: // Line 3
                    source2.PlayOneShot(clip2, 0.5f);
                    Debug.Log("Triggered event at line 3");
                    break;
                case 3: // Line 4
                    source2.PlayOneShot(clip3, 0.5f);
                    Debug.Log("Triggered event at line 4");
                    break;
                case 4: // Line 5
                    source2.PlayOneShot(clip4, 0.5f);
                    Debug.Log("Triggered event at line 5");
                    break;
            }
        }

        CloseDialogueBox();
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        sequenceManager.PartOneFinished();
    }
}