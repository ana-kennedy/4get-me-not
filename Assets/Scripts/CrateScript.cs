using System.Collections;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public GameObject CrateCollision;
    public GameObject CrateTexture;
    public GameObject BreakParticles;
    public AudioSource audioSource;
    public AudioClip clip1;
    public bool hasBroken = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !BallDash.canDash && !hasBroken)
        {

            hasBroken = true;

            CrateCollision.SetActive(false);
            CrateTexture.SetActive(false);

            // Play particle effects
            if (BreakParticles != null)
                BreakParticles.SetActive(true);

            // Play sound effect
            if (audioSource != null && clip1 != null)
                audioSource.PlayOneShot(clip1, 0.1f);

            // Disable particles after delay
            StartCoroutine(BreakParticle(1f));
        }
    }

    private IEnumerator BreakParticle(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (BreakParticles != null)
            BreakParticles.SetActive(false);
    }
}