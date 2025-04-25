using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class DialogueUIBalto1 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public GameObject BirdieDialogue1;
    public AudioClip clip;
    public GameObject Camera;
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
        if (i == 0 || i == 3 || i == 4)
        {
            if (i == 0)
            {
                 if (Camera != null)
            {
                // Disable CameraFollowObject
                CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null)
                {
                    followScript.enabled = false;
                }

                // Enable CameraFocusObject
                CameraFocusObject focusScript = Camera.GetComponent<CameraFocusObject>();
                if (focusScript != null)
                {
                    focusScript.enabled = true;
                }
            }
            }
            if (i == 3)
            {
                if(Camera != null)
                {
                    CameraFocusObject2 focusScript2 = Camera.GetComponent<CameraFocusObject2>();
                    if (focusScript2 != null)
                    {
                        focusScript2.enabled = true;
                    }

                    CameraFocusObject focusScript = Camera.GetComponent<CameraFocusObject>();
                    if (focusScript != null)
                    {
                        focusScript.enabled = false;
                    }
                
                }
            }
            if (i == 4)
            {
                if(Camera != null)
                {
                    CameraFocusObject2 focusScript2 = Camera.GetComponent<CameraFocusObject2>();
                    if (focusScript2 != null)
                    {
                        focusScript2.enabled = false;
                    }

                    CameraFocusObject focusScript = Camera.GetComponent<CameraFocusObject>();
                    if (focusScript != null)
                    {
                        focusScript.enabled = true;
                    }
                
                }
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
        BirdieDialogue1.SetActive(true);
        textLabel.text=string.Empty;

        if (Camera != null)
            {
                // Disable CameraFollowObject
                CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null)
                {
                    followScript.enabled = true;
                }

                // Enable CameraFocusObject
                CameraFocusObject focusScript = Camera.GetComponent<CameraFocusObject>();
                if (focusScript != null)
                {
                    focusScript.enabled = false;
                }
    }
    }

}
