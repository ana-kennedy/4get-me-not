using System.Collections;
using UnityEngine;

public class CannonScript2 : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject snowballPrefab; // Assign the Snowball prefab in the Inspector
    public Transform firePoint; // Assign a Transform for where the Snowball spawns
    public float shootInterval = 3f; // Time between shots
    public float snowballSpeed = 5f; // Speed of the Snowball

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip clip1;
    
    [Header("Detection Settings")]
    public Collider2D detectionZone; // Assign a trigger collider for ball detection
    private bool ballInRange = false;

    [System.Obsolete]
    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    [System.Obsolete]
    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (ballInRange) // Only shoot and play audio if the ball is in range
            {
                Shoot();
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }

    [System.Obsolete]
    private void Shoot()
    {
        if (audioSource != null && clip1 != null)
        {
            audioSource.PlayOneShot(clip1, 0.3f);
        }

        if (snowballPrefab != null && firePoint != null)
        {
            GameObject snowball = Instantiate(snowballPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = snowball.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(0, -snowballSpeed); // Shoot downward (-Y direction)
            }
        }
        else
        {
            Debug.LogWarning("Cannon: Missing Snowball Prefab or FirePoint!");
        }
    }

    // Detect when the ball enters the cannon's range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balto"))
        {
            ballInRange = true;
        }
    }

    // Detect when the ball leaves the cannon's range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Balto"))
        {
            ballInRange = false;
        }
    }
}