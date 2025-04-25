using UnityEngine;
using System.Collections;

public class CameraFocusZoom : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform targetObject; // Object to focus on

    [Header("Zoom Settings")]
    public float zoomInSize = 5f; // For Orthographic camera
    public float zoomInFOV = 20f; // For Perspective camera
    public float zoomSpeed = 2f;
    
    private Camera cam;
    private Vector3 originalPosition;
    private float originalSize;
    private float originalFOV;
    private bool isZoomedIn = false;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam == null)
        {
            Debug.LogError("CameraFocusZoom: No Camera component found on this GameObject.");
            return;
        }

        // Save the original camera settings
        originalPosition = transform.position;
        if (cam.orthographic)
        {
            originalSize = cam.orthographicSize;
        }
        else
        {
            originalFOV = cam.fieldOfView;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Press 'Z' to toggle zoom
        {
            if (!isZoomedIn)
                StartCoroutine(ZoomIn());
            else
                StartCoroutine(ZoomOut());
        }
    }

    private IEnumerator ZoomIn()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("CameraFocusZoom: No target object assigned.");
            yield break;
        }

        isZoomedIn = true;

        Vector3 targetPosition = targetObject.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), elapsedTime);
            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, elapsedTime);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomInFOV, elapsedTime);
            }
            
            elapsedTime += Time.deltaTime * zoomSpeed;
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        isZoomedIn = false;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, elapsedTime);
            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, elapsedTime);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalFOV, elapsedTime);
            }

            elapsedTime += Time.deltaTime * zoomSpeed;
            yield return null;
        }
    }
}