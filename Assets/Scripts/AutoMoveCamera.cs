using System.Collections;
using UnityEngine;

public class AutoMoveCamera : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed moving right
    public float returnSpeed = 5f; // Slower speed moving back left
    public Vector3 startPos = new Vector3(3, 0, -10); // Start at X = 3
    public Vector3 targetPos = new Vector3(180, 0, -10); // Moves right to X = 180
    public CameraFollow cameraFollowScript; // Reference to CameraFollow script

    private void Start()
    {
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = false; // Disable CameraFollow at start
        }

        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        // Move from startPos (3) to targetPos (180) to the right
        while (transform.position.x < targetPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2f); // Pause at X = 180

        // Move back from X = 180 to X = 3 to the left
        while (transform.position.x > startPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);
            yield return null;
        }

        // Reactivate CameraFollow script
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = true;
        }

        // Disable this script after completion
        gameObject.SetActive(false);
    }
}