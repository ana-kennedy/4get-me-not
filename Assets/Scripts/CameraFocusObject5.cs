using UnityEngine;

public class CameraFocusObject5 : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Assign the object to focus on
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Keep Z at -10 for 2D

    [Header("Camera Movement")]
    public float smoothSpeed = 5f; // Speed of camera movement
    private bool isAiming; // Detects when the player is dragging

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        
        if (cam != null)
        {
            cam.cullingMask = -1; // Ensure it renders all layers
        }
        else
        {
            Debug.LogWarning("Camera component not found on CameraFocusObject5!");
        }
        
        if (target == null)
        {
            Debug.LogError("CameraFocusObject5: Target is not assigned!");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Check if the player is aiming (dragging)
        isAiming = Input.GetMouseButton(0);

        if (!isAiming) // Only follow if NOT aiming
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = -10f; // Ensure the camera stays at the correct Z position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}