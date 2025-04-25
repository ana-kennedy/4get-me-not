using UnityEngine;

public class CameraFocusObjectZoomed2 : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1f, -5f);

    [Header("Camera Movement")]
    public float smoothSpeed = 7f;

    [Header("Camera Zoom")]
    public Camera mainCamera;
    public float zoomAmount = 4.5f; // Zoomed-in value
    private float defaultZoom; // Store the original zoom
    private bool isZoomedIn = false; // Track if zoom has been applied

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera.orthographic)
        {
            defaultZoom = mainCamera.orthographicSize;
        }
        else
        {
            defaultZoom = mainCamera.fieldOfView;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Apply zoom only once
        if (!isZoomedIn)
        {
            if (mainCamera.orthographic)
            {
                mainCamera.orthographicSize = zoomAmount;
            }
            else
            {
                mainCamera.fieldOfView = zoomAmount;
            }
            isZoomedIn = true; // Set zoomed in flag
        }
    }

    // New method to reset zoom
    public void ResetZoom()
    {
        if (mainCamera.orthographic)
        {
            mainCamera.orthographicSize = defaultZoom;
        }
        else
        {
            mainCamera.fieldOfView = defaultZoom;
        }
        isZoomedIn = false; // Reset zoomed in flag
        Debug.Log("Camera zoom reset to default.");
    }
}