using UnityEngine;

public class CameraFocusObject7 : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Assign your golf ball GameObject
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset to position the camera properly

    [Header("Camera Movement")]
    public float smoothSpeed = 5f; // Speed of camera movement
    private bool isAiming; // Detects when the player is dragging

    void LateUpdate()
    {
        if (target == null) return;

        // Check if the player is aiming (dragging)
        isAiming = Input.GetMouseButton(0);

        if (!isAiming) // Only follow the ball if NOT aiming
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}