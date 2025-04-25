using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetalScript : MonoBehaviour
{
    public Animator Transition;
    public GameObject TrObject;
    public GameObject EndText;
    public GameObject LoadingScreen;
    public GameObject AllUI1;
    public GameObject AllUI2;
    public GameObject AllUI3;
    public bool isTriggered = false;
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    public string oldQuest;
    public string newQuest;
    public string eventID;

    private TypingScript typingScript; // Reference to the Typewriter script

    [System.Obsolete]
    void Start()
    {
        // Automatically find TypewriterEffect on EndText GameObject
        if (EndText != null)
        {
            typingScript = EndText.GetComponent<TypingScript>();
        }
    }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && !isTriggered)
        {
            isTriggered = true;
            audioSource.PlayOneShot(clip1, 0.5f);
            
                if (!string.IsNullOrEmpty(oldQuest) && !string.IsNullOrEmpty(newQuest))
            {
                QuestManager.Instance.ReplaceQuest(oldQuest, newQuest);
            }
            if (!string.IsNullOrEmpty(eventID))
                {
                    GameManager.Instance.SetEventState(eventID, true);
                    Debug.Log("Event Triggered: " + eventID);
                }


            AllUI1.SetActive(false);
            AllUI2.SetActive(false);
            AllUI3.SetActive(false);
            TrObject.SetActive(true);
            Transition.SetBool("Active", true);

            // Get Ball's Rigidbody
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            BallDash ballDash = other.GetComponent<BallDash>(); // Reference to player movement script

            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero; // Stop all movement
                ballRb.constraints = RigidbodyConstraints2D.FreezeAll; // Completely freeze the ball
                Debug.Log("Ball frozen!");

                if (ballDash != null)
                {
                    ballDash.isMovementDisabled = true; // Disable input if using BallDash script
                    Debug.Log("Ball input disabled!");
                }
            }

            StartCoroutine(EndPhase(5f));
        }
    }

    private IEnumerator EndPhase(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip2, 0.7f);
        EndText.SetActive(true);

        // Start the Typewriter Dialogue Effect
        if (typingScript != null)
        {
            typingScript.StartTyping();
            Debug.Log("Typing dialogue started!");
        }

        StartCoroutine(TextOff(47.5f));
    }

    private IEnumerator TextOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndText.SetActive(false);

        StartCoroutine(BackToGirdwood(3f));
    }

    private IEnumerator BackToGirdwood(float delay)
    {
        yield return new WaitForSeconds(delay);
        TrObject.SetActive(false);
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("GWHub");
    }
}