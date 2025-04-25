using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for 2D Global Light

public class Thunderstorm : MonoBehaviour
{
    [Header("Lightning Settings")]
    public AudioSource audioSource;
    public AudioClip lightningStrike; // Assign thunder sound effect
    public AudioClip rainFx;
    public float lightningInterval = 7f; // Time between strikes
    public float lightFlashDuration = 0.2f; // How long the flash lasts
    public float lightFadeDuration = 1f; // How long it takes to fade back

    [Header("Global Light")]
    public Light2D globalLight; // Assign the Global Light 2D
    public float normalIntensity = 0f; // Default light intensity
    public float flashIntensity = 5f; // Light intensity when lightning strikes

    private void Start()
    {

        StartCoroutine(ThunderstormLoop());
        audioSource.PlayOneShot(rainFx, 0.5f);
    }

    private IEnumerator ThunderstormLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(lightningInterval);

            // Flash lightning effect
            if (audioSource != null && lightningStrike != null)
            {
                audioSource.PlayOneShot(lightningStrike, 0.8f);
            }

            StartCoroutine(LightningFlash());
        }
    }

    private IEnumerator LightningFlash()
    {
        // Flash the light
        globalLight.intensity = flashIntensity;
        yield return new WaitForSeconds(lightFlashDuration);

        // Gradually fade light back to normal
        float elapsedTime = 0f;
        while (elapsedTime < lightFadeDuration)
        {
            globalLight.intensity = Mathf.Lerp(flashIntensity, normalIntensity, elapsedTime / lightFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        globalLight.intensity = normalIntensity; // Ensure it's fully reset
    }
}