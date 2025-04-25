using UnityEngine;
using System.Collections;

public class HitBoxGeneralBalto : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip1;
    public float x;
    public float y;
    public float z;

    void Start()
    {
        // Automatically find "SoundManagement" and get its AudioSource
        GameObject soundManager = GameObject.Find("SoundManagement");
        if (soundManager != null)
        {
            audioSource = soundManager.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("SoundManagement GameObject not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Balto") && other.GetComponent<Rigidbody2D>() != null)
        {
            React(other.gameObject);

            if (audioSource != null && clip1 != null)
            {
                audioSource.PlayOneShot(clip1, 0.3f);
            }
        }
    }
    private void React(GameObject ball)
    {
        ball.transform.position = new Vector3(x, y, z);
    }
}