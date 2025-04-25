using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Assign your golf ball GameObject
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Ensure correct camera depth

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
            Debug.LogWarning("Camera component not found on CameraFollowObject!");
        }

        if (target == null)
        {
            Debug.LogError("CameraFollowObject: Target is not assigned!");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Check if the player is aiming (dragging)
        isAiming = Input.GetMouseButton(0);

        if (!isAiming) // Only follow the ball if NOT aiming
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = -10f; // Keep the camera at the correct depth
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}