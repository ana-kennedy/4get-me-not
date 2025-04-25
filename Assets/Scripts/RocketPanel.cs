using UnityEngine;

public class RocketPanel : MonoBehaviour
{
    public GameObject rocketPrefab; // Assign the Rocket prefab in the Inspector
    public Transform rocketSpawnPoint; // Assign an empty GameObject as the spawn position
    public float rocketSpeed = 10f; // Speed of the launched rocket
    public AudioSource audioSource;
    public AudioClip clip1;
    public bool hasLaunched = false;

    [Header("Rocket Rotation Settings")]
    public float rocketRotationZ = -90f; // Default to -90 for the first panel, set different values in Inspector for others

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !hasLaunched) // Check if the ball collides with the panel
        {
            hasLaunched = true;
            LaunchRocket();
        }
    }

    private void LaunchRocket()
    {
        audioSource.PlayOneShot(clip1, 0.5f);

        GameObject bigfoot = GameObject.FindWithTag("Bigfoot");

        // ✅ Set the correct rocket rotation based on the panel
        Quaternion rocketRotation = Quaternion.Euler(0, 0, rocketRotationZ);

        // Instantiate the rocket at the spawn point with the correct rotation
        GameObject rocket = Instantiate(rocketPrefab, rocketSpawnPoint.position, rocketRotation);
        Rocket rocketScript = rocket.GetComponent<Rocket>();

        if (rocketScript != null)
        {
            rocketScript.SetTarget(bigfoot.transform, rocketSpeed);
        }
    }
}