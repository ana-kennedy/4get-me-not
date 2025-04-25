using UnityEngine;

public class GnomePanel : MonoBehaviour
{
    public Animator panelAnimator; // Assign in Inspector
    public AudioSource audioSource;
    public AudioClip pressSound;

    private bool isPressed = false;
    private GnomeButtonSystem buttonSystem;

    [System.Obsolete]
    private void Start()
    {
        // Find the GnomeButtonSystem in the scene
        buttonSystem = FindObjectOfType<GnomeButtonSystem>();

        if (buttonSystem == null)
        {
            Debug.LogError("⚠️ GnomePanel: GnomeButtonSystem not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !isPressed)
        {
            isPressed = true;
            panelAnimator.SetBool("Pressed", true);

            if (audioSource != null && pressSound != null)
            {
                audioSource.PlayOneShot(pressSound, 0.5f);
            }

            // Notify the master script that this panel was pressed
            buttonSystem.PanelPressed();
        }
    }
}