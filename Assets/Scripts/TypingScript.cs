using System.Collections;
using UnityEngine;
using TMPro;

public class TypingScript : MonoBehaviour
{
    [Header("Text Settings")]
    public TMP_Text textComponent; // Assign your TMP UI Text Object
    public DialogueData dialogueData; // Assign DialogueData in the Inspector
    public float typingSpeed = 0.05f; // Adjust for slower or faster effect
    public float sentencePauseTime = 1f; // Pause time between sentences
    public bool startOnAwake = false; // If true, starts automatically
    private string fullText;
    private Coroutine typingCoroutine;

    [Header("Sound Settings")]
    public AudioSource audioSource; // Optional: Assign a sound source
    public AudioClip typingSound; // Optional: Assign a typing sound
    public float soundPitchVariance = 0.1f; // Optional: Add pitch variance for variety

    private void Awake()
    {
        if (textComponent == null)
        {
            Debug.LogError("TypewriterEffect: No TMP_Text assigned!");
        }

        if (dialogueData != null)
        {
            fullText = dialogueData.dialogueText;
        }

        if (startOnAwake && dialogueData != null)
        {
            StartTyping();
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
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textComponent.text = fullText; // Instantly reveal all text
    }
}