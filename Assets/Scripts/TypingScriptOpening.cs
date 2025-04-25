using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingScriptOpening : MonoBehaviour
{

    [Header("Objects")]
    public GameObject LoadingScreen;
    [Header("Text Settings")]
    public TMP_Text textComponent; // Assign your TMP UI Text Object
    public DialogueData dialogueData; // Assign DialogueData in the Inspector
    public float typingSpeed = 0.2f; // Adjust for slower or faster effect
    public float sentencePauseTime = 1f; // Pause time between sentences
    public bool startOnAwake = false; // If true, starts automatically
    private string fullText;
    private Coroutine typingCoroutine;

    [Header("Sound Settings")]
    public AudioSource audioSource; // Assign an AudioSource
    public AudioSource audioSource2;
    public AudioClip typingSound; // Assign a typing sound
    public AudioClip Soundtrack; // Assign the background music
    public float soundPitchVariance = 0.1f; // Optional: Add pitch variance for variety
    public float fadeOutDuration = 3.5f; // Duration for fading out the music

 private void Start()
{
    if (dialogueData != null && textComponent != null)
    {
        Invoke("StartTyping", 2f);
        audioSource2.PlayOneShot(Soundtrack, 0.5f);
    }
}
    private void Update()
    {
     if (Input.GetKeyDown(KeyCode.Return))
     {
        SkipTyping();
     }   
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (dialogueData != null)
        {
            fullText = dialogueData.dialogueText;
        }

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textComponent.text = "";
        
        for (int i = 0; i < fullText.Length; i++)
        {
            char letter = fullText[i];
            textComponent.text += letter;

            if (audioSource != null && typingSound != null)
            {
                audioSource.pitch = Random.Range(1f - soundPitchVariance, 1f + soundPitchVariance);
                audioSource.PlayOneShot(typingSound, 0.5f);
            }

            // If the character is a sentence-ending punctuation, pause
            if (letter == '.' || letter == '!' || letter == '?')
            {
                yield return new WaitForSeconds(sentencePauseTime);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // Dialogue finished, fade out music, then start LilTown transition
        StartCoroutine(FadeOutMusicAndLoadScene());
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textComponent.text = fullText; // Instantly reveal all text

        // Immediately fade out and transition
        StartCoroutine(FadeOutMusicAndLoadScene());
    }

    private IEnumerator FadeOutMusicAndLoadScene()
    {
        // Gradually reduce the volume over fadeOutDuration
        float startVolume = audioSource2.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource2.volume = Mathf.Lerp(startVolume, 0, t / fadeOutDuration);
            yield return null;
        }

        audioSource2.volume = 0; // Ensure it's fully faded out

        LoadingScreen.SetActive(true);

        // Start the scene transition after fading out
        StartCoroutine(LilTown(3f));
    }

    private IEnumerator LilTown(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Hub");
    }
}