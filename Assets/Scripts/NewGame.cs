using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGame : MonoBehaviour, IPointerClickHandler
{
    public GameObject LoadingScreen;
    public AudioSource audioSource;
    public AudioClip clip;

   public void OnPointerClick(PointerEventData eventData)
    {
            LoadingScreen.SetActive(true);
            audioSource.PlayOneShot(clip, 0.5f);

            StartCoroutine(OpeningCutscene(2f));

    }

    private IEnumerator OpeningCutscene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("OpeningCutscene");
    }

}
