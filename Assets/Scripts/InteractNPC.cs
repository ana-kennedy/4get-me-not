using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    [Header("NPC Settings")]
    public string npcID; // Unique NPC identifier (set in Inspector)
    public string eventID; // Unique Event ID (optional, set in Inspector)
    public string eventCondition; // Condition to trigger DialogueBox2 (set in Inspector)
    
    [Header("UI Elements")]
    public Animator InteractButton;
    public GameObject DialogueBox;
    public GameObject DialogueBox2; // Second DialogueBox triggered by event condition
    
    [Header("Audio Settings")]
    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip;
    public AudioClip questclip;
    
    [Header("Quest Replacement")]
    public string oldQuest; // Quest to be removed (set in Inspector)
    public string newQuest; // Quest to be added (set in Inspector)
    
    private bool playerIsClose;
    private bool hasTriggered;

    void Start()
    {
        InteractButton.SetBool("Default", true);
        
        if (GameManager.Instance != null)
        {
            hasTriggered = GameManager.Instance.GetNpcTriggered(npcID);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && CanInteract())
        {
            HandleInteraction();
        }
    }

    private bool CanInteract()
    {
        if (!hasTriggered)
        {
            return true;
        }
        
        // Allow re-interaction if eventCondition (BushmanDefeated) is met
        if (GameManager.Instance != null && !string.IsNullOrEmpty(eventCondition))
        {
            return GameManager.Instance.GetEventState(eventCondition);
        }
        
        return false;
    }

    private void HandleInteraction()
    {
        hasTriggered = true;
        
        // Determine which dialogue box to activate
        if (GameManager.Instance != null && !string.IsNullOrEmpty(eventCondition) && GameManager.Instance.GetEventState(eventCondition))
        {
            DialogueBox2?.SetActive(true);
        }
        else
        {
            DialogueBox?.SetActive(true);
        }
        
        source.PlayOneShot(clip, 0.5f);
        StoreInteractionState();
        HandleQuestReplacement();
    }

    private void StoreInteractionState()
    {
        if (GameManager.Instance == null) return;
        
        GameManager.Instance.SetNpcTriggered(npcID, true);
        
        if (!string.IsNullOrEmpty(eventID))
        {
            GameManager.Instance.SetEventState(eventID, true);
            Debug.Log("Event Triggered: " + eventID);
        }
    }

    private void HandleQuestReplacement()
    {
        if (string.IsNullOrEmpty(oldQuest) || string.IsNullOrEmpty(newQuest)) return;
        
        QuestManager.Instance.ReplaceQuest(oldQuest, newQuest);
        source2.PlayOneShot(questclip, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        InteractButton.SetBool("Default", false);
        InteractButton.SetBool("Enter", true);
        InteractButton.SetBool("Exit", false);
        playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        InteractButton.SetBool("Exit", true);
        InteractButton.SetBool("Enter", false);
        playerIsClose = false;
    }
}
