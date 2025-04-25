using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class TutorialDialogueUI : MonoBehaviour
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

        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));


        //Dialogue FX Sound
            if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            source.PlayOneShot(clip, 0.3f);
        }
        }

        

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }

}
