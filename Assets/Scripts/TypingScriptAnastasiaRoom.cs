using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingScriptAnastasiasRoom : MonoBehaviour
{
    [Header("Objects")]
    public GameObject LoadingScreen;
    
    [Header("Text Settings")]
    public TMP_Text textComponent;
    public DialogueData dialogueData;
    public float typingSpeed = 0.2f;
    public float sentencePauseTime = 1f;
    public bool startOnAwake = false;
    private string fullText;
    private Coroutine typingCoroutine;

    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip typingSound;
    public AudioClip Soundtrack;
    public float soundPitchVariance = 0.1f;
    public float fadeOutDuration = 3.5f;

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

            if (letter == '.' || letter == '!' || letter == '?')
            {
                yield return new WaitForSeconds(sentencePauseTime);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        StartCoroutine(FadeOutMusicAndLoadScene());
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        textComponent.text = fullText; 

        StartCoroutine(FadeOutMusicAndLoadScene());
    }

    private IEnumerator FadeOutMusicAndLoadScene()
    {
        float startVolume = audioSource2.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource2.volume = Mathf.Lerp(startVolume, 0, t / fadeOutDuration);
            yield return null;
        }

        audioSource2.volume = 0; 

        LoadingScreen.SetActive(true);

        // Clear all text when LoadingScreen activates**
        textComponent.text = ""; 

        StartCoroutine(BackToHotel(3f));
    }

    private IEnumerator BackToHotel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("NHotel");
    }
}