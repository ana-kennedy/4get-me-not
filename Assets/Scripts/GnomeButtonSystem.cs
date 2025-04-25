using UnityEngine;
using System.Collections;

public class GnomeButtonSystem : MonoBehaviour
{
    [Header("Gnome Wall Settings")]
    public GameObject GnomeWall;
    public Animator GnomeWallAni;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip wallDisableSound;

    [Header("Camera Settings")]
    public GameObject Camera;

    private int panelsPressed = 0;
    private bool wallDisabled = false;

    public void PanelPressed()
    {
        panelsPressed++;

        if (panelsPressed >= 2 && !wallDisabled)
        {
            wallDisabled = true;
            GnomeWallAni.SetBool("Disabled", true);

            BoxCollider2D wallCollider = GnomeWall.GetComponent<BoxCollider2D>();
            if (wallCollider != null)
            {
                wallCollider.enabled = false;
            }

            if (audioSource != null && wallDisableSound != null)
            {
                audioSource.PlayOneShot(wallDisableSound, 0.5f);
            }

            Debug.Log("✅ Both panels pressed! Gnome Wall Disabled.");

            StartCoroutine(CameraFocusSequence());
        }
    }

    private IEnumerator CameraFocusSequence()
    {
        var cameraFollow = Camera.GetComponent<CameraFollowObject>();
        var cameraFocus = Camera.GetComponent<CameraFocusObject3>();

        if (cameraFollow != null && cameraFocus != null)
        {
            cameraFollow.enabled = false;
            cameraFocus.enabled = true;

            yield return new WaitForSeconds(2f);

            cameraFocus.enabled = false;
            cameraFollow.enabled = true;
        }
        else
        {
            Debug.LogWarning("Missing CameraFollowObject or CameraFocusObject3 on the Camera GameObject.");
        }
    }
}