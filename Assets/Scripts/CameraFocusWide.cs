using System.Collections;
using UnityEngine;

public class CameraFocusZoomOut : MonoBehaviour
{
    public Transform target; // The object the camera will focus on
    public float focusSpeed = 2f; // Speed of camera movement
    public float zoomOutSize = 20f; // Target camera size when zooming out
    public float zoomSpeed = 2f; // Speed of zooming out
    public bool returnToOriginal = false; // Should the camera return to its original state?
    public float returnDelay = 3f; // Delay before returning

    private Camera cam;
    private Vector3 originalPosition;
    private float originalSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("No Camera component found!");
            return;
        }

        // Store original position and size
        originalPosition = transform.position;
        originalSize = cam.orthographicSize;

        // Start focusing and zooming
        StartCoroutine(FocusAndZoomOut());
    }

    private IEnumerator FocusAndZoomOut()
    {
        // Move camera towards target
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), focusSpeed * Time.deltaTime);
            yield return null;
        }

        // Zoom out smoothly
        while (Mathf.Abs(cam.orthographicSize - zoomOutSize) > 0.1f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        // Optional: Return to original position & zoom after delay
        if (returnToOriginal)
        {
            yield return new WaitForSeconds(returnDelay);
            StartCoroutine(ReturnToOriginal());
        }
    }

    private IEnumerator ReturnToOriginal()
    {
        // Move camera back to its original position
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, focusSpeed * Time.deltaTime);
            yield return null;
        }

        // Zoom back in smoothly
        while (Mathf.Abs(cam.orthographicSize - originalSize) > 0.1f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }
}