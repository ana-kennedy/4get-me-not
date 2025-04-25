using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DialogueUIBirdieGirdwood : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public bool isOpen { get; private set; }
    public AudioSource source;
    public ShakeData explosionShakeData;
    public AudioClip clip;
    public AudioClip  clip2;
    public AudioClip  clip3;
    public GameObject TrObject;
    public Animator Transition;



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
        if (i == 6)
        {
           source.PlayOneShot(clip2, 0.7f);
           CameraShakerHandler.Shake(explosionShakeData);
        }
    }

    CloseDialogueBox();
}

    private void CloseDialogueBox()
    {
        GameManager.Instance.SaveGame();
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
        TrObject.SetActive(true);
        Transition.SetBool("Active", true);
        source.PlayOneShot(clip3, 0.5f);
        StartCoroutine(StartGolf(1.5f));
    }

    private IEnumerator StartGolf(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GWH1");
    }

}
