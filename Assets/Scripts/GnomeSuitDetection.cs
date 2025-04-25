using System.Collections;
using UnityEngine;

public class GnomeSuitDetection : MonoBehaviour
{
    public GameObject notPassedDbox;
    public GameObject passedDbox;
    public bool hasPassed = false;
    public static bool hasTried = false;
    public AudioSource audioSource;
    public AudioClip clip1;
    public float x;
    public float y;
    public GameObject Ball;
    public GameObject TeleportParticle;

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            MonoBehaviour movementScript = other.GetComponent<MonoBehaviour>(); // Replace with actual movement script

            if (ballRb != null)
            {
                StartCoroutine(DisableMovementTemporarily(ballRb, movementScript));
            }

            if (GnomeHatSpawner.hasTriggered && !hasPassed)
            {
                hasPassed = true;
                passedDbox.SetActive(true);
            }
            else
            {
                if(!hasTried)
                {
                notPassedDbox.SetActive(true);
                hasTried = true;
                }
                if(hasTried)
                {
                    Ball.transform.position = new Vector2(x, y);
                    TeleportParticle.SetActive(true);
                    audioSource.PlayOneShot(clip1, 0.5f);
                    StartCoroutine(EndTeleport(0.3f));
                }
            }
        }
    }

    [System.Obsolete]
    IEnumerator DisableMovementTemporarily(Rigidbody2D rb, MonoBehaviour movementScript)
    {
        // Disable movement
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Stops physics interactions
        if (movementScript != null)
        {
            movementScript.enabled = false; // Disable movement script if found
        }

        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        // Re-enable movement
        rb.isKinematic = false;
        if (movementScript != null)
        {
            movementScript.enabled = true; // Re-enable movement script
        }
    }

     private IEnumerator EndTeleport(float delay)
    {
        yield return new WaitForSeconds(delay);
        TeleportParticle.SetActive(false);
    }
}