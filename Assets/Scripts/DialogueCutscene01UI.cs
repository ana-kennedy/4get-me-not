using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DialogueCutscene01UI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public GameObject LoadingScreen;
    public GameObject QuestsUI;
    public GameObject MenuUI;
    public AudioClip clip;



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
            source.PlayOneShot(clip);
            StartCoroutine(NextLevel());
        }
        }

        

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.Instance.SavePlayerPosition(SceneManager.GetActiveScene().name, player.transform.position);
        }
        else
        {
            Debug.LogWarning("SavePlayerPosition: Player not found in the scene!");
        }

        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }

    // Scene Loading
    IEnumerator NextLevel()
    {
        LoadingScreen.SetActive(true);
        MenuUI.SetActive(false);
        QuestsUI.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("TheCaveOutside");
    }
}
