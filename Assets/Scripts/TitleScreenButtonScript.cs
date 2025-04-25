using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScreenButtonScript : MonoBehaviour, IPointerClickHandler
{

public AudioSource audioSource;
public AudioClip clip;
public GameObject LoadingScreen;

    public void OnPointerClick(PointerEventData eventData)
    {

            audioSource.PlayOneShot(clip);
            StartCoroutine(NextLevel());
    }

    // Scene Loading
    IEnumerator NextLevel()
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Menu");
    }

}