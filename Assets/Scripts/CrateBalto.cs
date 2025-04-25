using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CrateBalto : MonoBehaviour
{
    public GameObject Crate;
    public AudioSource audioSource;
    public AudioClip explosionFX;
    public AudioClip zapFX;

    void Start()
    {
        if (audioSource == null)
        {
            GameObject soundManager = GameObject.Find("SoundManagement");
            if (soundManager != null)
            {
                audioSource = soundManager.GetComponent<AudioSource>();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Balto"))
        {
            if(BaltoRide.isHeadbutting)
            {
                Collider2D crateCollider = Crate.GetComponent<Collider2D>();
                if (crateCollider != null)
                {
                    crateCollider.enabled = false;
                }

                SpriteRenderer crateRenderer = Crate.GetComponent<SpriteRenderer>();
                if (crateRenderer != null)
                {
                    Color color = crateRenderer.color;
                    crateRenderer.color = new Color(color.r, color.g, color.b, 0f);
                }

                audioSource.PlayOneShot(explosionFX, 0.5f);
                
            }
            else
            {
                React();
                audioSource.PlayOneShot(zapFX, 0.5f);
            }
        }
    }


    private void React()
    {
        GameObject balto = GameObject.FindGameObjectWithTag("Balto");
        if (balto != null)
        {
            Vector3 baltoPos = balto.transform.position;
            balto.transform.position = new Vector3(baltoPos.x, baltoPos.y - 10f, baltoPos.z);
        }
    }
}
