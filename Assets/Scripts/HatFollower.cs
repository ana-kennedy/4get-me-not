using UnityEngine;

public class HatFollower : MonoBehaviour
{
    public Transform target; // Now public so it can be set from outside
    public Vector3 offset = new Vector3(0f, 0.5f, 0f); // Adjust the hat position above the Ball

    void Start()
    {
        // If target isn't set manually, find the Ball automatically
        if (target == null)
        {
            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            if (ball != null)
            {
                target = ball.transform;
            }
            else
            {
                Debug.LogWarning("No GameObject with tag 'Ball' found! Destroying hat.");
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}