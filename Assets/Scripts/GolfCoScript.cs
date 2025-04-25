using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GolfCoScript : MonoBehaviour
{
    public bool PlayerIsClose;
    public GameObject LoadingScreen;
    public GameObject MenuUI;
    public GameObject QuestsUI;
    public AudioSource audioSource;
    public AudioClip clip;
    public GameObject BuildingTitle;

    void Update()
    {
    // Scene Load Trigger
    if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose && GameManager.Instance.GetEventState("BushmanDefeated"))
    {
        
         GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.Instance.SavePlayerPosition(SceneManager.GetActiveScene().name, player.transform.position);
        }
        else
        {
            Debug.LogWarning("SavePlayerPosition: Player not found in the scene!");
        }

        audioSource.PlayOneShot(clip, 0.5f);
        StartCoroutine(NextLevel()); // Start the coroutine when the player presses "E"

        BuildingTitle.SetActive(PlayerIsClose);
    }
    }

    private IEnumerator NextLevel()
    {
        LoadingScreen.SetActive(true);
        QuestsUI.SetActive(false);
        MenuUI.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Golfco");
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
