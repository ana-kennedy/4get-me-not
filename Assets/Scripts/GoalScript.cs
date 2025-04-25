using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public GameObject LevelCompleteUI;
    public GameObject RestartNextUI;
    public GameObject Particles;
    public GameObject Soundtrack;
    public GameObject GolfUI;
    public GameObject BitCountUI;
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    public bool HasHitGoal = false;
    public bool FoundEveryBit = false;

    private void Update()
    {
        // Check if the player has collected all bits
        int collectedBits = GameManager.Instance.GetBitCount();
        int totalBits = GameManager.Instance.GetTotalBits(); // Assuming this represents the max bits available

        // Update FoundEveryBit accordingly
        FoundEveryBit = (collectedBits >= totalBits);
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!HasHitGoal)
        {
            Debug.Log("Triggered Goal");
            HasHitGoal = true;
            audioSource.PlayOneShot(clip1, 0.3f);
            LevelCompleteUI.SetActive(true);
            RestartNextUI.SetActive(true);
            Particles.SetActive(true);
            GolfUI.SetActive(false);
            BitCountUI.SetActive(true);

            // Play a special sound if all bits were found
            if (FoundEveryBit)
            {
                audioSource.PlayOneShot(clip2, 0.5f);
            }

            // Disable the AudioSource component on the "Soundtrack" GameObject
            if (Soundtrack != null)
            {
                AudioSource soundtrackAudio = Soundtrack.GetComponent<AudioSource>();
                if (soundtrackAudio != null)
                {
                    soundtrackAudio.enabled = false;
                }
            }

            // Stop the ball's movement
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero;
            }
        }
    }
}