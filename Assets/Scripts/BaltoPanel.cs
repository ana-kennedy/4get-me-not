using UnityEngine;

public class BaltoPanel : MonoBehaviour
{
    public AudioClip boostSFX;
    private AudioSource audioSource;
    public float boostSpeed = 100f;
    public float boostDuration = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balto"))
        {
            BaltoRide baltoRide = other.GetComponent<BaltoRide>();
            Animator baltoAnimator = other.GetComponent<Animator>();
            if (baltoRide != null)
            {
                StartCoroutine(BoostBalto(baltoRide, baltoAnimator));
            }
        }
    }

    private System.Collections.IEnumerator BoostBalto(BaltoRide baltoRide, Animator baltoAnimator)
    {
        float originalSpeed = baltoRide.forwardSpeed;
        baltoRide.forwardSpeed = boostSpeed;
        if (baltoAnimator != null)
        {
            baltoAnimator.SetBool("isDashing", true);
        }

        if (audioSource != null && boostSFX != null)
        {
            audioSource.PlayOneShot(boostSFX);
        }

        yield return new WaitForSeconds(boostDuration);

        baltoRide.forwardSpeed = originalSpeed;
        if (baltoAnimator != null)
        {
            baltoAnimator.SetBool("isDashing", false);
        }
    }
}
