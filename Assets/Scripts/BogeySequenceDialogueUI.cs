using UnityEngine;
using System.Collections;
using TMPro;

public class BogeySequenceDialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;
    public Animator Bogey;

    private typewriterEffect typewriterEffect;
    public bool isClosed = false;

    private BogeySequence bogeySequence; // Reference to BogeySequence

    [System.Obsolete]
    private void Start()
    {
        typewriterEffect = GetComponent<typewriterEffect>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);

        // Find the BogeySequence script in the scene
        bogeySequence = FindObjectOfType<BogeySequence>();

        if (bogeySequence == null)
        {
            Debug.LogWarning("BogeySequence not found in the scene!");
        }
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;    
        Bogey.SetBool("Active", true);
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

            // Dialogue FX Sound
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.PlayOneShot(clip, 0.3f);
            }
        }
        
        CloseDialogueBox();
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        isClosed = true;
        Bogey.SetBool("Active", false);

        // Call ResetCamera in BogeySequence
        if (bogeySequence != null)
        {
            bogeySequence.ResetCamera();
        }
    }
}