using System.Collections;
using UnityEngine;

public class EnterNomeDetect : MonoBehaviour
{
    public GameObject NomeLogo;
    public string oldQuest;
    public string newQuest;
    public string eventID;
    public AudioSource audioSource;
    public AudioClip questclip;
    
    static public bool hasTriggeredNome = false;
    private bool cooldownActive = false;

    void Start()
    {
        hasTriggeredNome = PlayerPrefs.GetInt("hasTriggeredNome", 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggeredNome && !cooldownActive)
        {
            StartCoroutine(TriggerCooldown());
            hasTriggeredNome = true;
            PlayerPrefs.SetInt("hasTriggeredNome", 1);
            PlayerPrefs.Save();

            if (!string.IsNullOrEmpty(oldQuest) && !string.IsNullOrEmpty(newQuest))
            {
                QuestManager.Instance.ReplaceQuest(oldQuest, newQuest);
                audioSource.PlayOneShot(questclip, 0.2f);
                Invoke("AddNomeEvent", 0.2f);
            }

            NomeLogo.SetActive(true);
            StartCoroutine(NomeLogoSwitch(3f));
        }
    }

    private IEnumerator NomeLogoSwitch(float delay)
    {
        yield return new WaitForSeconds(delay);
        NomeLogo.SetActive(false);
          
        if (!string.IsNullOrEmpty(eventID))
        {
            Debug.Log("Event Triggered: " + eventID);
        }
    }

    private void AddNomeEvent()
    {
        GameManager.Instance.SetEventState(eventID, true);
    }

    private IEnumerator TriggerCooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(0.1f); // Prevent duplicate triggers
        cooldownActive = false;
    }
}