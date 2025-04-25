using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    public float rotationSpeed = 100f; // Degrees per second
    private float currentRotation = 0f;

    void Update()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationStep);
        
        currentRotation += rotationStep;
        if (currentRotation >= 360f)
        {
            currentRotation -= 360f; // Reset the rotation counter
        }
    }
}
