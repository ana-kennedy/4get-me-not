using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class AnastasiaRoomCutsceneDialogue2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;

    private typewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<typewriterEffect>();
        CloseDialogueBox();
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
        float totalTime = 30f;
        int dialogueCount = dialogueObject.Dialogue.Length;
        float interval = totalTime / dialogueCount;

        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitForSeconds(interval);

            // Dialogue FX Sound
            source.PlayOneShot(clip, 0.3f);
        }

        CloseDialogueBox();
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }
}
