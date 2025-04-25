using UnityEngine;
using System.Collections;

public class MicroDetection : MonoBehaviour
{
    public GameObject TeleportParticle;
    public AudioSource audioSource;
    public AudioClip zap;
    public float x;
    public float y;
    public float z;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        // Lock toggle when entering zone
        BallMicroSize.isToggleLocked = true;

        // If not micro, teleport the player
        if (!BallMicroSize.isMicro)
        {
            React(other.gameObject);
            StartCoroutine(Teleport(1f));
            TeleportParticle.SetActive(true);
            audioSource.PlayOneShot(zap, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // Unlock toggle on exit
            BallMicroSize.isToggleLocked = false;
        }
    }

    private void React(GameObject ball)
    {
        ball.transform.position = new Vector3(x, y, z);
    }

    private IEnumerator Teleport(float delay)
    {
        yield return new WaitForSeconds(delay);
        TeleportParticle.SetActive(false);
    }
}