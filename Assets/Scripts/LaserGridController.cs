using System.Collections;
using UnityEngine;

public class LaserGridController : MonoBehaviour
{
    [Header("Laser Grid Components")]
    public GameObject Grid;
    public GameObject GridLight;
    public Animator GridAni;
    public AudioSource audioSource;
    public AudioClip GridFX;

    private BoxCollider2D gridCollider;

    [Header("Timings")]
    public float activeDuration = 2f; // How long the laser stays active
    public float inactiveDuration = 4f; // How long the laser stays inactive

    private void Start()
    {
        if (Grid != null)
        {
            gridCollider = Grid.GetComponent<BoxCollider2D>();
        }
        else
        {
            Debug.LogWarning("⚠️ LaserGridController: Grid GameObject is not assigned!");
        }

        StartCoroutine(GridLoop());
    }

    private IEnumerator GridLoop()
    {
        while (true) // Infinite loop
        {
            yield return StartCoroutine(GridOn());
            yield return StartCoroutine(GridOff());
        }
    }

    private IEnumerator GridOn()
    {
        Debug.Log("✅ Laser Grid Activated");

        GridAni.SetBool("Active", true);
        GridAni.SetBool("UnActive", false);

        if (gridCollider != null)
        {
            gridCollider.enabled = true;
        }

        GridLight.SetActive(true);

        yield return StartCoroutine(GridSound()); // Ensure sound finishes
        yield return new WaitForSeconds(activeDuration); // Keep active for set duration
    }

    private IEnumerator GridOff()
    {
        Debug.Log("❌ Laser Grid Deactivated");

        GridAni.SetBool("Active", false);
        GridAni.SetBool("UnActive", true);

        if (gridCollider != null)
        {
            gridCollider.enabled = false;
        }

        GridLight.SetActive(false);

        yield return new WaitForSeconds(inactiveDuration); // Keep inactive for set duration
    }

    private IEnumerator GridSound()
    {
        if (audioSource != null && GridFX != null)
        {
            for (int i = 0; i < 4; i++) // Plays the sound 4 times
            {
                audioSource.PlayOneShot(GridFX, 0.3f);
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            Debug.LogWarning("⚠️ LaserGridController: AudioSource or GridFX is missing!");
        }
    }
}