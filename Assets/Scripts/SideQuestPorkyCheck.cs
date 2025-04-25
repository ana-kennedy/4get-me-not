using UnityEngine;
using System.Collections;

public class SideQuestPorkyCheck : MonoBehaviour
{
public GameObject DialogueBox;
public GameObject Soundtrack;
public GameObject BitUI;
public AudioSource audioSource;
public AudioClip collectFX;



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.Instance.GetEventState("CollectedBone") && !GameManager.Instance.GetEventState("FinishedPorkyQuest"))
        {
            GameManager.Instance.SetEventState("FinishedPorkyQuest", true);
            audioSource.PlayOneShot(collectFX, 0.5f);
            BitUI.SetActive(true);
            Soundtrack.SetActive(false);
            GameManager.Instance.AddBit();
            StartCoroutine(BitUIOff(5f));
        }
    }
    private IEnumerator BitUIOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        BitUI.SetActive(false);
        Soundtrack.SetActive(true);
        DialogueBox.SetActive(true);
    }

}
