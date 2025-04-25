using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class MamaGnomeDialogueUI4 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;
    public AudioClip laughFX;
    public AudioClip toneFX1;
    public AudioClip toneFX2;
    public AudioClip toneFX3;
    public bool isFinished = false;
    public bool spawnAnastasia = false;

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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.PlayOneShot(clip, 0.3f);
            }

            // Handle specific dialogue lines
            if (i == 1)
            {
                Debug.Log("Dialogue line 1 triggered.");
                source.PlayOneShot(laughFX, 0.7f);
            }
            else if (i == 3)
            {
                Debug.Log("Dialogue line 3 triggered.");
                source.PlayOneShot(toneFX1, 0.7f);
            }
            else if (i == 4)
            {
                Debug.Log("Dialogue line 4 triggered.");
                source.PlayOneShot(toneFX2, 0.7f);
            }
            else if (i == 5)
            {
                Debug.Log("Dialogue line 5 triggered.");
                source.PlayOneShot(toneFX3, 0.7f);
            }
            else if (i == 6)
            {
                Debug.Log("Dialogue line 6 triggered.");
                spawnAnastasia = true;
            }
        }

        CloseDialogueBox();
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
        isFinished = true;
    }
}
