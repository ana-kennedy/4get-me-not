using UnityEngine;

public class SnowballBalto : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public AudioClip clip1;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("SoundManagement").GetComponent<AudioSource>();

        // ✅ Destroy the Snowball automatically after 3 seconds
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure only objects tagged "Ball" AND with Rigidbody2D trigger it
        if (other.CompareTag("Balto") && other.GetComponent<Rigidbody2D>() != null)
        {
            React(other.gameObject);
            audioSource.PlayOneShot(clip1, 0.3f);
        }
    }

    private void React(GameObject balto)
    {
        balto.transform.position = new Vector3(x, y, z);
    }
}