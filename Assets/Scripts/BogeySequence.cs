using UnityEngine;

public class BogeySequence : MonoBehaviour
{
    public GameObject DialogueBox;
    public Camera Camera;
    public bool hasTriggered = false;

    private CameraFocusObjectZoomed cameraFocus;
    private CameraFollow cameraFollow;
    private BogeySequenceDialogueUI dialogueUI;

    void Start()
    {
        if (Camera != null)
        {
            cameraFocus = Camera.GetComponent<CameraFocusObjectZoomed>();
            cameraFollow = Camera.GetComponent<CameraFollow>();

            Debug.Log("CameraFollow: " + (cameraFollow != null ? "Found" : "Not Found"));
            Debug.Log("CameraFocus: " + (cameraFocus != null ? "Found" : "Not Found"));
        }
    }

    void Update()
    {
        if (dialogueUI != null)
        {
            Debug.Log("DialogueUI isClosed: " + dialogueUI.isClosed);
            if (dialogueUI.isClosed)
            {
                ResetCamera();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("BogeySequence Triggered!");
            Sequence();
        }
    }

    void Sequence()
    {
        hasTriggered = true;
        DialogueBox.SetActive(true);
        
        if (cameraFollow != null)
        {
            cameraFollow.enabled = false;
            Debug.Log("CameraFollow Disabled!");
        }
        else
        {
            Debug.LogWarning("CameraFollow is NULL!");
        }

        if (cameraFocus != null)
        {
            cameraFocus.enabled = true;
            Debug.Log("CameraFocus Enabled!");
        }
        else
        {
            Debug.LogWarning("CameraFocus is NULL!");
        }
    }

    public void ResetCamera()
    {
        if (cameraFollow != null) cameraFollow.enabled = true;
        if (cameraFocus != null)
        {
            cameraFocus.enabled = false;
            cameraFocus.ResetZoom(); // Restore the default zoom
        }
        Debug.Log("Camera reset: CameraFollow enabled, CameraFocus disabled, Zoom reset.");
    }
}