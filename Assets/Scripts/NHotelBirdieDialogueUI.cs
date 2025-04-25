using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class NHotelBirdieDialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioClip clip;
    public GameObject LoadingUI;
    public GameObject QuestsUI;
    public GameObject MenuUI;



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

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
        MenuUI.SetActive(false);
        QuestsUI.SetActive(false);
        LoadingUI.SetActive(true);

        StartCoroutine(LevelLoaded(2f));
    }

    private IEnumerator LevelLoaded(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("NH1");
    }

}
