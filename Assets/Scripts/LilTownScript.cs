using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LilTownScript : MonoBehaviour
{
    public bool PlayerIsClose;
    public GameObject LoadingScreen;
    public AudioSource audioSource;
    public AudioClip clip;

    void Update()
    {
    // Scene Load Trigger
    if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(NextLevel()); // Start the coroutine when the player presses "E"
    }
    }

    // Scene Loading
    IEnumerator NextLevel()
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Hub");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
        }
    }
}