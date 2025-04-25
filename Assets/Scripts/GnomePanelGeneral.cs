using UnityEngine;

public class GnomePanelGeneral : MonoBehaviour
{
    [Header("Panel Settings")]
    public Animator panelAnimator;
    public AudioSource audioSource;
    public AudioClip pressSound;

    [Header("References")]
    public GnomeButtonSystemGeneral buttonSystem; 

    private bool isPressed = false;

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

            if (buttonSystem != null)
            {
                buttonSystem.PanelPressed();
            }
            else
            {
                Debug.LogWarning("⚠️ GnomePanel: No GnomeButtonSystem assigned.");
            }
        }
    }
}