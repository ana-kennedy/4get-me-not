using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target;
    private float speed;

    public void SetTarget(Transform targetTransform, float rocketSpeed)
    {
        target = targetTransform;
        speed = rocketSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy rocket if Bigfoot is missing
            return;
        }

        // Move towards Bigfoot
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bigfoot")) // Check if it hits Bigfoot
        {
            Destroy(gameObject); // Destroy rocket on impact
        }
    }
}