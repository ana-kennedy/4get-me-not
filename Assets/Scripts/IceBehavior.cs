using UnityEngine;

public class IceBehavior : MonoBehaviour
{
    public float kickForce = 12f; // Force when kicked back
    private bool canBeKicked = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            canBeKicked = true; // Ice lands and becomes kickable
        }
        else if (collision.gameObject.CompareTag("Bigfoot")) 
        {
            Destroy(gameObject); // Destroy when colliding with Bigfoot
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeKicked && collision.gameObject.CompareTag("Ball")) 
        {
            Rigidbody2D iceRb = GetComponent<Rigidbody2D>();
            if (iceRb != null)
            {
                Vector2 directionToBigfoot = (GameObject.FindWithTag("Bigfoot").transform.position - transform.position).normalized;
                iceRb.velocity = directionToBigfoot * kickForce; // Kick back towards Bigfoot
            }
        }
    }
}