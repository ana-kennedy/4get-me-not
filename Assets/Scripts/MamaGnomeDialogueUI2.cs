using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class MamaGnomeDialogueUI2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private MamaGnomeSequence sequenceManager;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip;
    public AudioClip StabFX;
    public AudioClip Laugh1;
    public AudioClip Laugh2;
    public AudioClip FX1;
    public AudioClip FX2;
    public AudioClip FX3;

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
        source2.PlayOneShot(StabFX, 0.5f);
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
                source.PlayOneShot(clip, 0.5f);
            }

            
            switch (i)
            {
                case 0: //Line 1
                    source2.PlayOneShot(Laugh1, 0.3f);
                    break;
                case 7: // Line 8
                    source2.PlayOneShot(Laugh2, 0.2f);
                    break;
                case 10: // Line 11
                    source2.PlayOneShot(FX1, 0.5f);
                    break;
                case 11: //Line 12
                    source2.PlayOneShot(FX2, 0.5f);
                    break;
                case 12: //Line 13
                    source2.PlayOneShot(FX3, 0.5f);
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
        sequenceManager.PartTwoFinished();
    }
}