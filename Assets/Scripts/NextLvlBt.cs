using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextLvlBt : MonoBehaviour, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip clip1;
    public GameObject LoadingScreen;
    public GameObject AllUI;
    public GameObject AllUI2;
    public GameObject AllUI3;
    public GameObject AllUI4;

    public string LevelName; // Set this in the Inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button Clicked! Trying to load: " + LevelName);

        if (audioSource != null && clip1 != null)
        {
            audioSource.PlayOneShot(clip1);
        }
        else
        {
            Debug.LogWarning("AudioSource or Clip is missing in NextLvlBt!");
        }

        StartLevelTransition();
    }

    private void StartLevelTransition()
    {
        if (LoadingScreen != null) LoadingScreen.SetActive(true);
        AllUI.SetActive(false);
        AllUI2.SetActive(false);
        AllUI3.SetActive(false);
        AllUI4.SetActive(false);
        
        Invoke("NextLevel", 3.0f);
    }

    private void NextLevel()
    {
        if (!string.IsNullOrEmpty(LevelName) && Application.CanStreamedLevelBeLoaded(LevelName))
        {
            Debug.Log("Loading scene: " + LevelName);
            SceneManager.LoadScene(LevelName);
        }
        else
        {
            Debug.LogError("Scene '" + LevelName + "' is not found! Check Build Settings.");
        }
    }
}
