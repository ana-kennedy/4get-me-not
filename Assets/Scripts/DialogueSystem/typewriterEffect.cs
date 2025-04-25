using UnityEngine;
using System.Collections;
using TMPro;

public class typewriterEffect : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public float typingSpeed = 0.05f;
    
    // Pitch variation range
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    public IEnumerator Run(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        foreach (char letter in textToType)
        {
            textLabel.text += letter;

            if (letter != ' ') // optional: skip spaces
            {
                source.pitch = Random.Range(minPitch, maxPitch);
                source.PlayOneShot(clip, 0.2f);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        // Reset pitch afterward
        source.pitch = 1f;
    }
}