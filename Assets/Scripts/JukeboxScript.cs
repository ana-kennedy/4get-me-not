using UnityEngine;
using UnityEngine.SceneManagement;

public class JukeboxScript : MonoBehaviour
{
    public Animator InteractButton;
    public AudioSource audioSource; // Jukebox's AudioSource
    public AudioClip clip;  // Interaction sound
    public AudioClip clip2; // Jukebox music
    public bool PlayerIsClose = false;
    private bool isPlaying = false;

    private PersistentAudioManager persistentAudio; // Reference to Persistent Audio Manager

    [System.Obsolete]
    void Start()
    {
        InteractButton.SetBool("Default", true);
        
        // Find the Persistent Audio Manager in the scene (if it exists)
        persistentAudio = FindObjectOfType<PersistentAudioManager>();

        // Stop the persistent soundtrack if Jukebox was active in the previous scene
        if (persistentAudio != null && isPlaying)
        {
            persistentAudio.audioSource.Pause();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
            audioSource.PlayOneShot(clip, 0.5f); // Play interaction sound

            if (!isPlaying)
            {
                // Stop Persistent Soundtrack when Jukebox is activated
                if (persistentAudio != null && persistentAudio.audioSource.isPlaying)
                {
                    persistentAudio.audioSource.Pause();
                }

                audioSource.clip = clip2;
                audioSource.volume = 0.7f;
                audioSource.loop = true; // Keep Jukebox looping
                audioSource.Play();
                isPlaying = true;
            }
            else
            {
                // Stop Jukebox and resume Persistent Soundtrack
                audioSource.Stop();
                isPlaying = false;

                if (persistentAudio != null)
                {
                    persistentAudio.audioSource.UnPause(); // Resume soundtrack
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
            PlayerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetBool("Exit", true);
            InteractButton.SetBool("Enter", false);
            PlayerIsClose = false;
        }
    }

    private void OnDestroy()
    {
        // ✅ When leaving the scene, stop the Jukebox and resume Persistent Soundtrack
        if (isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
        }

        if (persistentAudio != null)
        {
            persistentAudio.audioSource.UnPause(); // Always resume soundtrack when leaving scene
        }
    }
}