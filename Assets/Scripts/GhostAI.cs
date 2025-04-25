using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float speed = 2f; // Speed of the ghost
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}