using System.Collections;
using UnityEngine;

public class JailKeyScript : MonoBehaviour
{
    public GameObject JailKeyDoor;
    public GameObject JailKey;
    public AudioSource audioSource;
    public AudioClip activateFX;
    public GameObject Camera;
    public GameObject DialogueBox;
    public static bool hasTriggered = false;
    public string eventID;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && !hasTriggered)
        {
            hasTriggered = true;
            audioSource.PlayOneShot(activateFX, 0.5f);
            JailKeyDoor.SetActive(false);
            StartCoroutine(CameraSegment(0.1f));
        }
    }

    private IEnumerator CameraSegment(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(Camera != null)
        {
            CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
            if(followScript != null)
            {
                followScript.enabled = false;
            }
            CameraFocusObject7 focusScript = Camera.GetComponent<CameraFocusObject7>();
            if(focusScript != null)
            {
                focusScript.enabled = true;
            }

            yield return new WaitForSeconds(2f);

            if(followScript != null)
            {
                followScript.enabled = true;
            }
            if(focusScript != null)
            {
                focusScript.enabled = false;
            }

            yield return new WaitForSeconds(0.1f);

            JailKey.SetActive(false);
            DialogueBox.SetActive(true);
        }
        
    }
}
