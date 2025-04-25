using System.Collections;
using UnityEngine;

public class EnterGirdwoodDetect : MonoBehaviour
{

    public GameObject GirdwoodLogo;
    public Animator GirdwoodAni;
    public string oldQuest; // Quest to be removed (set in Inspector)
    public string newQuest; // Quest to be added (set in Inspector)
    public string eventID; // Unique Event ID (optional, set in Inspector)
    public AudioSource audioSource;
    public AudioClip questclip;
    static public bool hasTriggered = false;
    

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
          
            
            if (!string.IsNullOrEmpty(oldQuest) && !string.IsNullOrEmpty(newQuest) && !GameManager.Instance.GetEventState("EnteredGirdwood") && !hasTriggered)
            {
                hasTriggered = true;
                QuestManager.Instance.ReplaceQuest(oldQuest, newQuest);
                audioSource.PlayOneShot(questclip, 0.2f);
                Invoke("AddGirdwoodEvent", 0.2f);
            }

            GirdwoodLogo.SetActive(true);
            GirdwoodAni.SetBool("Active", true);
            StartCoroutine(GirdwoodLogoSwitch(5f));

        }
    }

    private IEnumerator GirdwoodLogoSwitch(float delay)
    {
        yield return new WaitForSeconds(delay);
        GirdwoodLogo.SetActive(false);
          
        if (!string.IsNullOrEmpty(eventID))
            {
                
                Debug.Log("Event Triggered: " + eventID);
            }
    }
    private void AddGirdwoodEvent()
    {
        GameManager.Instance.SetEventState(eventID, true);
    }
}
