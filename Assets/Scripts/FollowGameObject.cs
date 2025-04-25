using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public Transform target; // Assign the GameObject to follow in the Inspector
    public float followSpeed = 5f; // Adjust the speed of following
    public float stoppingDistance = 0.5f; // Distance to stop following

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            // Move towards the target if it's further than the stopping distance
            if (distance > stoppingDistance)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
            }
        }
    }
}