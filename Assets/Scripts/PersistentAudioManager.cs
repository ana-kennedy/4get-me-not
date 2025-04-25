using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PersistentAudioManager : MonoBehaviour
{
    public static PersistentAudioManager Instance;
    public AudioSource audioSource;
    public List<string> allowedScenes; // Scenes where the music continues playing

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (allowedScenes.Contains(scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Continue playing if not already playing
            }
        }
        else
        {
            audioSource.Stop(); // Stop playing if scene is not in the list
            Destroy(gameObject); // Destroy the object so it doesn't persist in unwanted scenes
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }
}