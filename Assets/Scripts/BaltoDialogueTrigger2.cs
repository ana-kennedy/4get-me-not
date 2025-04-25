using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class BaltoDialogueTrigger2 : MonoBehaviour
{

    public GameObject DialogueBox;
    public GameObject MamaGnome;
    public GameObject Camera;
    public AudioSource audioSource;
    public AudioClip interactFX;
    public bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Balto") && !hasTriggered)
        {
            hasTriggered = true;
            DialogueBox.SetActive(true);
            MamaGnome.SetActive(true);
            audioSource.PlayOneShot(interactFX, 0.5f);

            if(Camera != null)
            {
                CameraFocusObjectZoomed2 cameraFocus = Camera.GetComponent<CameraFocusObjectZoomed2>();
                cameraFocus.zoomAmount = 6.5f;
            }
        }
    }


}
