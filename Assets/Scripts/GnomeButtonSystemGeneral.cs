using UnityEngine;
using System.Collections;

public class GnomeButtonSystemGeneral : MonoBehaviour
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
    [Header("Camera Focus Options")]
    public MonoBehaviour[] cameraFocusOptions; 
    public int CameraFocusIndex; 

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

    if (cameraFollow == null)
    {
        Debug.LogWarning("CameraFollowObject is missing on the Camera.");
        yield break;
    }

    int index = Mathf.Clamp(CameraFocusIndex, 0, cameraFocusOptions.Length - 1);

    if (cameraFocusOptions.Length > 0 && cameraFocusOptions[index] != null)
    {
        MonoBehaviour selectedFocus = cameraFocusOptions[index];

        cameraFollow.enabled = false;
        selectedFocus.enabled = true;

        yield return new WaitForSeconds(2f);

        selectedFocus.enabled = false;
        cameraFollow.enabled = true;
    }
    else
    {
        Debug.LogWarning("Camera focus option at index is missing or not assigned.");
    }
}
}