using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CabinScript : MonoBehaviour
{
public bool playerisClose = false;
public GameObject LoadingScreenUI;
public GameObject QuestsUI;
public GameObject HomeUI;

    void Update()
    {
        if(Input.GetKeyDown("KeyCode.E") && GameManager.Instance.GetEventState("NaomiTalked1") && playerisClose)
        {
            HomeUI.SetActive(false);
            QuestsUI.SetActive(false);
            LoadingScreenUI.SetActive(true);
            StartCoroutine(LoadLevel(2f));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerisClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerisClose = false;
        }
    }

    private IEnumerator LoadLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GCabin");
    }
}
