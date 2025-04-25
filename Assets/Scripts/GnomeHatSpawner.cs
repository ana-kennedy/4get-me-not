using UnityEngine;

public class GnomeHatSpawner : MonoBehaviour
{
    public GameObject gnomeHatPrefab; 
    public GameObject triggerLight;
    public Vector3 hatOffset = new Vector3(0f, 0.5f, 0f);
    public Animator PanelAni;
    static public bool hasTriggered = false;
    public AudioSource audioSource;
    public AudioClip clip1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !hasTriggered)
        {
            // Check if the ball already has a hat
            if (other.GetComponent<HatFollower>() == null)
            {
                // Instantiate the hat
                GameObject newHat = Instantiate(gnomeHatPrefab, other.transform.position + hatOffset, Quaternion.identity);
                
                // Attach a HatFollower script to track the Ball
                HatFollower hatFollower = newHat.AddComponent<HatFollower>();
                hatFollower.target = other.transform;
                hatFollower.offset = hatOffset;
                PanelAni.SetBool("isPressed", true);
                hasTriggered = true;
                audioSource.PlayOneShot(clip1, 0.5f);
                triggerLight.SetActive(true);
            }
        }
    }
}