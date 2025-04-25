using System.Collections;
using UnityEngine;

public class SideQuestPorky : MonoBehaviour
{

    public AudioClip collectFX;
    public AudioSource audioSource;
    public GameObject BoneUI;
    public GameObject BoneUI2;
    public GameObject Soundtrack;
    public bool playerisClose = false;
    public Animator InteractButton;


    void Update()
    {
        if(playerisClose && GameManager.Instance.GetEventState("TalkedPorky1") && Input.GetKeyDown(KeyCode.E) && !GameManager.Instance.GetEventState("CollectedBone"))
        {
            BoneUI.SetActive(true);
            BoneUI2.SetActive(true);
            Soundtrack.SetActive(false);
            audioSource.PlayOneShot(collectFX, 0.5f);
            GameManager.Instance.SetEventState("CollectedBone", true);
            StartCoroutine(CollectSequence(5f));
        }
    }

    private IEnumerator CollectSequence(float delay)
    {
        yield return new WaitForSeconds(delay);
        BoneUI.SetActive(false);
        BoneUI2.SetActive(false);
        Soundtrack.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerisClose = true;
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerisClose = false;
            InteractButton.SetBool("Exit", true);
            InteractButton.SetBool("Enter", false);
        }
    }
}
