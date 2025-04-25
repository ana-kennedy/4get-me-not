using UnityEngine;
using System.Collections;

public class RedTileScript : MonoBehaviour
{
    public GameObject TeleportParticle;
    public AudioSource audioSource;
    public AudioClip clip1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure only objects tagged "Ball" AND with Rigidbody2D trigger it
        if (other.CompareTag("Ball") && other.GetComponent<Rigidbody2D>() != null)
        {
            React(other.gameObject);
            StartCoroutine(Teleport(1f, other.gameObject));
            TeleportParticle.SetActive(true);
            audioSource.PlayOneShot(clip1, 0.3f);
        }
    }

    private void React(GameObject ball)
    {
        ball.transform.position = new Vector3(-9.9f, 32, -2);
    }

    private IEnumerator Teleport(float delay, GameObject ball)
    {
        yield return new WaitForSeconds(delay);
        TeleportParticle.SetActive(false);
    }
}