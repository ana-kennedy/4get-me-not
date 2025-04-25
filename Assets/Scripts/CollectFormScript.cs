using System.Collections;
using UnityEngine;

public class CollectFormScript : MonoBehaviour
{

    public GameObject BlackAndWhite;
    public GameObject FormUI;
    public GameObject FormUI2;
    public GameObject Form;
    public GameObject Platform;
    public GameObject blockPlatform;
    public GameObject ParticleSystem;
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    private bool isFormHidden = false;

    private void Start()
    {
        blockPlatform.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFormHidden) // Ensures it doesn't retrigger while waiting
        {
            isFormHidden = true;
            Form.SetActive(false);
            FormUI.SetActive(true);
            FormUI2.SetActive(true);
            audioSource.PlayOneShot(clip1, 0.3f);
            Platform.SetActive(true);
            blockPlatform.SetActive(false);
            BlackAndWhite.SetActive(true);
            ParticleSystem.SetActive(true);

            StartCoroutine(InForm(4f));
        }
    }

    private IEnumerator InForm(float delay)
    {
        yield return new WaitForSeconds(delay);
        BlackAndWhite.SetActive(false);
        Form.SetActive(true);
        FormUI.SetActive(false);
        FormUI2.SetActive(false);
        Platform.SetActive(false);
        blockPlatform.SetActive(true);
        ParticleSystem.SetActive(false);
        audioSource.PlayOneShot(clip2, 0.3f);
        isFormHidden = false; // Reset to allow retriggering
    }
}