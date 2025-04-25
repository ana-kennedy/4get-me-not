using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CaveEntrance : MonoBehaviour
{
    public bool PlayerIsClose;
    public GameObject LoadingScreen;
    public GameObject InteractButton;
    public AudioSource audioSource;
    public AudioClip clip;
    public GameObject BuildingTitle;

   void Update()
    {
    // Scene Load Trigger
    if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(NextLevel()); // Start the coroutine when the player presses "E"
    }

    // Building UI Names
    BuildingTitle.SetActive(PlayerIsClose);
    }

    // Scene Loading
    IEnumerator NextLevel()
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("TheCaveInside");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = true;
            InteractButton.SetActive(true);
            BuildingTitle.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
            InteractButton.SetActive(false);
            BuildingTitle.SetActive(false);
        }
    }
}