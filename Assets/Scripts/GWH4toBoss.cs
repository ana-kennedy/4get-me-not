using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;


public class GWH4toBoss : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    public GameObject TransitionObject;
    public Animator Transition;
    public GameObject AllUI1;
    public GameObject AllUI2;
    public string oldQuest;
    public string newQuest;



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
    AllUI1.SetActive(false);
    AllUI2.SetActive(false);
    dialogueBox.SetActive(false);
    textLabel.text = string.Empty;
    TransitionObject.SetActive(true);
    Transition.SetBool("Active", true);

    if (!string.IsNullOrEmpty(oldQuest) && !string.IsNullOrEmpty(newQuest))
            {
                QuestManager.Instance.ReplaceQuest(oldQuest, newQuest);
            }

    if (source.isPlaying)
    {
        source.Stop();
    }

    // Play the second clip
    source.PlayOneShot(clip2, 0.5f);

    // Delay scene transition
    Invoke("EndSegment", 2f);
}

    private void EndSegment()
    {
        SceneManager.LoadScene("GWBoss");
    }
}
