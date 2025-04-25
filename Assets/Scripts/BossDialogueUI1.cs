using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class BossDialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public Animator Bigfoot;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public GameObject Camera;
    public GameObject Soundtrack;
    public GameObject BossHP;

    private typewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<typewriterEffect>();
        ShowDialogue(testDialogue);

        // Ensure Soundtrack doesn't play on start
        if (Soundtrack != null)
        {
            AudioSource soundtrackSource = Soundtrack.GetComponent<AudioSource>();
            if (soundtrackSource != null)
            {
                soundtrackSource.Stop();
            }
        }
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
            if (i == 2)
            {
                source.PlayOneShot(clip3, 0.5f);
                Bigfoot.SetBool("isLaughing", true);

                if (Camera != null)
                {
                    // Disable CameraFollowObject
                    CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                    if (followScript != null)
                    {
                        followScript.enabled = false;
                    }

                    // Enable CameraFocusObject
                    CameraFocusObject2 focusScript = Camera.GetComponent<CameraFocusObject2>();
                    if (focusScript != null)
                    {
                        focusScript.enabled = true;
                    }
                    StartCoroutine(RevertChanges(2f));
                }
            }
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        BossHP.SetActive(true);
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        Bigfoot.SetBool("isLaughing", false);

        // Play Soundtrack Audio Source
        if (Soundtrack != null)
        {
            AudioSource soundtrackSource = Soundtrack.GetComponent<AudioSource>();
            if (soundtrackSource != null)
            {
                soundtrackSource.Play();
            }
        }
    }

    private IEnumerator RevertChanges(float delay)
    {
        Debug.Log("RevertChanges Coroutine Started!");
        yield return new WaitForSeconds(delay);

        if (Camera != null)
        {
            // Re-enable CameraFollowObject
            CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
            if (followScript != null)
            {
                followScript.enabled = true;
            }

            // Disable CameraFocusObject
            CameraFocusObject2 focusScript = Camera.GetComponent<CameraFocusObject2>();
            if (focusScript != null)
            {
                focusScript.enabled = false;
            }
        }
    }
}