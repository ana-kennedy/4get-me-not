using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class BirdieTriggerGeneral : MonoBehaviour
{

    public GameObject Birdie;
    public AudioSource source;
    public bool isTriggered = false;
    public AudioClip clip;
    public ShakeData explosionShakeData;
    public ShakeData explosionShakeData2;

    void Update()
    {
        if (isTriggered == true)
        {
            Birdie.SetActive(true);
        }
        if (isTriggered == false)
        {
            Birdie.SetActive(false);

        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            source.PlayOneShot(clip, 0.5f);
            CameraShakerHandler.Shake(explosionShakeData);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
        source.PlayOneShot(clip, 0.5f);
        CameraShakerHandler.Shake(explosionShakeData2);
    }
}