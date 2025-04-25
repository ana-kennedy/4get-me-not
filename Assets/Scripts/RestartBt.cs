using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartBt : MonoBehaviour, IPointerClickHandler
{

public AudioSource audioSource;
public AudioClip clip1;
public GameObject LoadingScreen;
public GameObject AllUI;
public GameObject AllUI2;
public GameObject AllUI3;
public GameObject AllUI4;

public void OnPointerClick(PointerEventData eventData)
{
audioSource.PlayOneShot(clip1);
StartCoroutine(RestartLevel());
}

IEnumerator RestartLevel()
    {
        LoadingScreen.SetActive(true);
        AllUI.SetActive(false);
        AllUI2.SetActive(false);
        AllUI3.SetActive(false);
        AllUI4.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("GirdwoodHole01");
    }
}
