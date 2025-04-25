using System.Collections;
using UnityEngine;

public class GiftWeightPanelScript2 : MonoBehaviour
{
public GameObject Gate;
public bool hasTriggered = false;
public AudioSource audioSource;
public AudioClip activationClip;
public GameObject cameraObject;
private Component cameraFocus;
private Component cameraFollow;


    void Awake()
    {
             // Cache camera components
        if (cameraObject != null)
        {
            cameraFocus = cameraObject.GetComponent("CameraFocusObject3");
            cameraFollow = cameraObject.GetComponent("CameraFollowObject");

            if (cameraFocus == null || cameraFollow == null)
            {
                Debug.LogWarning("Missing CameraFocusObject3 or CameraFollowObject on Camera.");
            }
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Gift") && !hasTriggered)
        {
            Gate.SetActive(false);
            audioSource.PlayOneShot(activationClip, 0.5f);
            hasTriggered = true;

            if (cameraFocus != null && cameraFollow != null)
        {
            ((Behaviour)cameraFocus).enabled = true;
            ((Behaviour)cameraFollow).enabled = false;
        }

            StartCoroutine(CameraChange(2f));
        }
    }

    private IEnumerator CameraChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (cameraFocus != null && cameraFollow != null)
        {
            ((Behaviour)cameraFocus).enabled = false;
            ((Behaviour)cameraFollow).enabled = true;
        }
    }
}
