using UnityEngine;
using System.Collections;

public class RedTileScriptHole3Set3 : MonoBehaviour
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
        ball.transform.position = new Vector3(30.44f, 13.88f, -2);
    }

    private IEnumerator Teleport(float delay, GameObject ball)
    {
        yield return new WaitForSeconds(delay);
        TeleportParticle.SetActive(false);
    }
}