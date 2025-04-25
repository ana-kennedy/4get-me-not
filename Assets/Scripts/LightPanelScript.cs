using UnityEngine;

public class LightPanelScript : MonoBehaviour
{
 public AudioSource audioSource;
 public AudioClip lightSound;
 public GameObject Light;
 public bool onOff = false;

    void Update()
    {
        if(!onOff)
        {
            Light.SetActive(false);
        }   
        else
        {
            Light.SetActive(true);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball"))
        {
            onOff = !onOff;
            audioSource.PlayOneShot(lightSound, 0.5f);
        }
        if(other.CompareTag("Snowball"))
        {
            onOff = !onOff;
        }
    }
}
