using System.Collections;
using UnityEngine;

public class OpeningScriptMenu : MonoBehaviour
{
 
 public GameObject PCSound;
 public GameObject PCLogo;
 public GameObject TerminalText;
 public GameObject Soundtrack;
 public GameObject Video;


    void Awake()
    {
        StartCoroutine(PetalCo(1.0f));   
    }

    private IEnumerator PetalCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        PCSound.SetActive(true);
        PCLogo.SetActive(true);
        yield return new WaitForSeconds(6f);
        PCLogo.SetActive(false);
        PCSound.SetActive(false);
        TerminalText.SetActive(true);
        yield return new WaitForSeconds(5f);
        Soundtrack.SetActive(true);
        Video.SetActive(true);
    }
}
