using UnityEngine;
using System.Collections;

public class RedTileScriptGeneral : MonoBehaviour
{
    public GameObject TeleportParticle;
    public AudioSource audioSource;
    public AudioClip clip1;
    public float x;
    public float y;
    public float z;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure only objects tagged "Ball" AND with Rigidbody2D trigger it
        if (other.CompareTag("Ball") && other.GetComponent<Rigidbody2D>() != null)
        {
            Debug.Log("Touched Lava, Teleporting");
            React(other.gameObject);
            StartCoroutine(Teleport(1f, other.gameObject));
            TeleportParticle.SetActive(true);
            audioSource.PlayOneShot(clip1, 0.3f);
        }
    }

    private void React(GameObject ball)
    {
        ball.transform.position = new Vector3(x, y, z);
    }

    private IEnumerator Teleport(float delay, GameObject ball)
    {
        yield return new WaitForSeconds(delay);
        TeleportParticle.SetActive(false);
    }
}