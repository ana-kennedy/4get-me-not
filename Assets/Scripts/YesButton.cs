using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class YesButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject LoadingScreen;
    public GameObject MenuUI;
    public GameObject QuestsUI;
    public AudioSource audioSource;
    public AudioClip clip;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioSource.PlayOneShot(clip, 0.2f);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.Instance.SavePlayerPosition(SceneManager.GetActiveScene().name, player.transform.position);
        }
        else
        {
            Debug.LogWarning("SavePlayerPosition: Player not found in the scene!");
        }
        
        // Show loading screen & hide UI instantly
        LoadingScreen.SetActive(true);
        MenuUI.SetActive(false);
        QuestsUI.SetActive(false);

        // Invoke the scene load after 3 seconds
        Invoke("NextLevel", 3.0f);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}